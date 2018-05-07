using Isac.Api.Configuration;
using Isac.Api.Extensions;
using Isac.Api.Integrations;
using Isac.Api.Models;
using Isac.Api.Models.Crucible;
using Isac.Api.Models.Crucible.Enums;
using Isac.Api.Models.FishEye;
using Isac.Api.Properties;
using Isac.Api.Utilities;
using Isac.WebHooks.Receivers.BitbucketServer.Models;
using Isac.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isac.Api.Services
{
    public class PullRequestService : IPullRequestService
    {
        private readonly IBitbucketClient bitbucketClient;
        private readonly ICrucibleClient crucibleClient;
        private readonly IFishEyeClient fishEyeClient;
        private readonly IntegrationsConfig integrations;
        private readonly SettingsConfig settings;

        public PullRequestService(IBitbucketClient bitbucketClient, ICrucibleClient crucibleClient, IFishEyeClient fishEyeClient, 
            IOptions<IntegrationsConfig> integrations, IOptions<SettingsConfig> settings)
        {
            this.bitbucketClient = bitbucketClient;
            this.crucibleClient = crucibleClient;
            this.fishEyeClient = fishEyeClient;
            this.integrations = integrations.Value;
            this.settings = settings.Value;
        }

        public async Task OnOpenedHandlerAsync(PullRequestOpenedNotification notification)
        {
            Guard.AgainstNullArgument<PullRequestOpenedNotification>(nameof(notification), notification);

            await this.AddBotAsReviewer(notification);

            if (await this.HasMergeConflicts(notification.PullRequest))
            {
                // update the pr with needs_work
                await this.bitbucketClient.SetReviewerStatus(notification.PullRequest, new BitbucketParticipant()
                {
                    User = new BitbucketUser()
                    {
                        Name = this.integrations.Bitbucket.Credentials.UserName,
                        Slug = this.integrations.Bitbucket.Credentials.UserName
                    },
                    IsApproved = false,
                    Status = BitbucketStatus.NeedsWork
                });
                // comment with, please fix merge conflicts and add steps for fix
                await this.bitbucketClient.AddComment(notification.PullRequest, new BitbucketComment()
                {
                    Text = string.Format(Resources.PullRequest_MergeConflictResolution, notification.PullRequest.ToReference.DisplayName,
                        notification.PullRequest.FromReference.DisplayName)
                });

                //return;
            }

            // check other criteria
            FishEyeChangesets Changesets = await this.FindReviewsByCommits(notification.PullRequest);

            // check all commits are associated with a review
            await this.AreAllCommitsReviewed(notification.PullRequest, Changesets);
            // check for closed review
            // check for 2 reviewers in complete status
            await this.ValidateReviewConditions(notification.PullRequest, Changesets);            
        }

        private async Task AddBotAsReviewer(PullRequestOpenedNotification notification)
        {
            if (!this.IsReviewer(notification.PullRequest.Reviewers))
            {
                // add the bot as a reviewer
                await this.bitbucketClient.AddReviewer(notification.PullRequest, new BitbucketParticipant()
                {
                    User = new BitbucketUser()
                    {
                        Name = this.integrations.Bitbucket.Credentials.UserName
                    },
                    Role = BitbucketRole.Reviewer
                });
            }
        }

        private bool IsReviewer(List<BitbucketParticipant> reviewers)
        {
            return reviewers.Exists(reviewer =>
            {
                return reviewer.User.Name.Equals(this.integrations.Bitbucket.Credentials.UserName);
            });
        }

        private async Task<bool> HasMergeConflicts(BitbucketPullRequest pullRequest)
        {
            BitbucketMergeStatus Response = await this.bitbucketClient.TestMerge(pullRequest);

            // TODO, may need to handle vetos
            return Response.IsConflicted;
        }

        private async Task<FishEyeChangesets> FindReviewsByCommits(BitbucketPullRequest pullRequest)
        {
            BitbucketPageResponse<BitbucketPullRequestCommit> Response = await this.bitbucketClient.GetCommits(pullRequest);

            return await this.fishEyeClient.GetReviewsForChangesets(pullRequest.FromReference.Repository.Slug,
                Response.Values.Select(s => s.Id).ToList());
        }

        private async Task<bool> AreAllCommitsReviewed(BitbucketPullRequest pullRequest, FishEyeChangesets changesets)
        {
            List<FishEyeChangeset> ChangesetsMissingReviews = changesets.Changesets.FindAll(c => c.Reviews.Count <= 0);

            if (ChangesetsMissingReviews.Count > 0)
            {
                // list commits that are not associated with a review
                await this.bitbucketClient.AddComment(pullRequest, new BitbucketComment()
                {
                    Text = string.Format(Resources.PullRequest_CommitsNotAssociatedWithAReview, 
                        string.Join("\n", ChangesetsMissingReviews.Select(s => $"* {s.ChangesetId}")))
                });
            }

            return false;
        }

        private async Task ValidateReviewConditions(BitbucketPullRequest pullRequest, FishEyeChangesets changesets)
        {
            // find distinctive reviews from changesets
            List<FishEyeReview> FishEyeReviews = new List<FishEyeReview>();
            List<Problem> Problems;

            foreach (FishEyeChangeset Changeset in changesets.Changesets)
            {
                FishEyeReviews.AddRange(Changeset.Reviews.GroupBy(g => g.ProjectKey).Select(r => r.First()));
            }

            // get details on the reviews
            List<CrucibleReview> CrucibleReviews = new List<CrucibleReview>();

            foreach (FishEyeReview Review in FishEyeReviews)
            {
                CrucibleReviews.Add(await this.crucibleClient.GetReviewDetails(Review.PermaId["id"].ToString()));
            }

            // check if all reviews are closed and have 2 completed reviewers
            Problems = this.ValidateStateForAssociatedReviews(CrucibleReviews);
            Problems = Problems.Merge(this.ValidateCompletedReviewersForAssociatedReviews(CrucibleReviews));

            // if failure found, comment to the pr the problem, make sure to check both though
            if (Problems.Count > 0)
            {
                await this.bitbucketClient.SetReviewerStatus(pullRequest, new BitbucketParticipant()
                {
                    User = new BitbucketUser()
                    {
                        Name = this.integrations.Bitbucket.Credentials.UserName,
                        Slug = this.integrations.Bitbucket.Credentials.UserName
                    },
                    IsApproved = false,
                    Status = BitbucketStatus.NeedsWork
                });
                await this.bitbucketClient.AddComment(pullRequest, new BitbucketComment()
                {
                    Text = string.Format(Resources.PullRequest_NeedsWorkReviewConditions, pullRequest.Author.User.Name, 
                        string.Join("\n", Problems.Select(s => $"* {s.Message}")))
                });
            }
        }

        private List<Problem> ValidateStateForAssociatedReviews(List<CrucibleReview> reviews)
        {
            return reviews
                .Where(w => w.State != this.settings.PullRequests.ReviewConditions.ReviewState)
                .Select(s => new Problem()
                {
                    Key = s.PermanentId["id"].ToString(),
                    ResourceUrl = this.integrations.Crucible.Urls.Review,
                    Message = string.Format(Resources.PullRequest_ReviewStateValidation,
                        this.settings.PullRequests.ReviewConditions.ReviewState.ToString())
                })
                .ToList<Problem>();
        }

        private List<Problem> ValidateCompletedReviewersForAssociatedReviews(List<CrucibleReview> reviews)
        {
            return reviews
                .Where(w => w.Reviewers.Reviewers.Count(c => c.HasCompleted) < this.settings.PullRequests.ReviewConditions.ReviewerCount)
                .Select(s => new Problem()
                {
                    Key = s.PermanentId["id"].ToString(),
                    ResourceUrl = this.integrations.Crucible.Urls.Review,
                    Message = string.Format(Resources.PullRequest_ReviewCompletedReviewers, 
                        this.settings.PullRequests.ReviewConditions.ReviewerCount)
                })
                .ToList<Problem>();
        }
    }
}
