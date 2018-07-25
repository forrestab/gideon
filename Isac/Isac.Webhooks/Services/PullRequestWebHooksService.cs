using Isac.Common;
using Isac.Common.Configuration;
using Isac.Common.Models;
using Isac.Integrations.Atlassian.Bitbucket;
using Isac.Integrations.Atlassian.Bitbucket.Models;
using Isac.Integrations.Atlassian.Bitbucket.Models.Enums;
using Isac.Integrations.Atlassian.Bitbucket.Models.Notifications;
using Isac.Integrations.Atlassian.Crucible;
using Isac.Integrations.Atlassian.Crucible.Models;
using Isac.Integrations.Atlassian.FishEye;
using Isac.Integrations.Atlassian.FishEye.Models;
using Isac.WebHooks.Properties;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isac.WebHooks.Services
{
    public class PullRequestWebHooksService : IPullRequestWebHooksService
    {
        private readonly IntegrationsConfig integrations;
        private readonly SettingsConfig settings;
        private readonly IBitbucketService bitbucket;
        private readonly IFishEyeService fishEye;
        private readonly ICrucibleService crucible;

        public PullRequestWebHooksService(IOptions<IntegrationsConfig> integrations, IOptions<SettingsConfig> settings, 
            IBitbucketService bitbucket, IFishEyeService fishEye, ICrucibleService crucible)
        {
            this.integrations = integrations.Value;
            this.settings = settings.Value;
            this.bitbucket = bitbucket;
            this.fishEye = fishEye;
            this.crucible = crucible;
        }

        public async Task OnOpenedAsync(PullRequestOpenedNotification notification)
        {
            Guard.AgainstNullArgument<PullRequestOpenedNotification>(nameof(notification), notification);

            await this.bitbucket.AddReviewerAsync(notification.PullRequest, this.integrations.Bitbucket.Credentials.UserName);

            if(await this.bitbucket.CheckForMergeConflictsAsync(notification.PullRequest))
            {
                // update the pr with needs_work
                await this.bitbucket.SetReviewerStatusAsync(notification.PullRequest, new BitbucketParticipant()
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
                await this.bitbucket.AddCommentAsync(notification.PullRequest, new BitbucketComment()
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

        private async Task<FishEyeChangesets> FindReviewsByCommits(BitbucketPullRequest pullRequest)
        {
            BitbucketPageResponse<BitbucketPullRequestCommit> Response = await this.bitbucket.GetCommitsAsync(pullRequest);

            return await this.fishEye.FindReviewsByCommitIds(pullRequest.FromReference.Repository.Slug,
                Response.Values.Select(s => s.Id).ToList());
        }

        private async Task<bool> AreAllCommitsReviewed(BitbucketPullRequest pullRequest, FishEyeChangesets changesets)
        {
            List<FishEyeChangeset> ChangesetsMissingReviews = changesets.Changesets.FindAll(c => c.Reviews.Count <= 0);

            if (ChangesetsMissingReviews.Count > 0)
            {
                // list commits that are not associated with a review
                await this.bitbucket.AddCommentAsync(pullRequest, new BitbucketComment()
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
                CrucibleReviews.Add(await this.crucible.GetReviewDetailsAsync(Review.PermaId["id"].ToString()));
            }

            // check if all reviews are closed and have 2 completed reviewers
            Problems = this.ValidateStateForAssociatedReviews(CrucibleReviews);
            Problems = Problems.Merge(this.ValidateCompletedReviewersForAssociatedReviews(CrucibleReviews));

            // if failure found, comment to the pr the problem, make sure to check both though
            if (Problems.Count > 0)
            {
                await this.bitbucket.SetReviewerStatusAsync(pullRequest, new BitbucketParticipant()
                {
                    User = new BitbucketUser()
                    {
                        Name = this.integrations.Bitbucket.Credentials.UserName,
                        Slug = this.integrations.Bitbucket.Credentials.UserName
                    },
                    IsApproved = false,
                    Status = BitbucketStatus.NeedsWork
                });

                await this.bitbucket.AddCommentAsync(pullRequest, new BitbucketComment()
                {
                    Text = string.Format(Resources.PullRequest_NeedsWorkReviewConditions, pullRequest.Author.User.Name, 
                        string.Join("\n", Problems.Select(s => $"* {s.Message}")))
                });
            }
        }

        private List<Problem> ValidateStateForAssociatedReviews(List<CrucibleReview> reviews)
        {
            return reviews
                // TODO, not sure I like this
                .Where(w => w.State.ToString() != this.settings.PullRequests.ReviewConditions.ReviewState)
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
