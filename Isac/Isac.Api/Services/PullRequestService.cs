using Isac.Api.Integrations;
using Isac.Api.Models;
using Isac.Api.Models.FishEye;
using Isac.Api.Properties;
using Isac.Api.Utilities;
using Isac.WebHooks.Receivers.BitbucketServer.Models;
using Isac.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Isac.Api.Services
{
    public class PullRequestService : IPullRequestService
    {
        private readonly IBitbucketClient bitbucketClient;
        private readonly IFishEyeClient fishEyeClient;

        public PullRequestService(IBitbucketClient bitbucketClient, IFishEyeClient fishEyeClient)
        {
            this.bitbucketClient = bitbucketClient;
            this.fishEyeClient = fishEyeClient;
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
                        // TODO, pull this from configuration
                        Name = "isac-bot",
                        Slug = "isac-bot"
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
            await this.AreAllCommitsReviewed(notification.PullRequest);
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
                        // TODO, pull this from configuration
                        Name = "isac-bot"
                    },
                    Role = BitbucketRole.Reviewer
                });
            }
        }

        private bool IsReviewer(List<BitbucketParticipant> reviewers)
        {
            return reviewers.Exists(reviewer =>
            {
                // TODO, pull bot name from configuration
                return reviewer.User.Name.Equals("isac-bot");
            });
        }

        private async Task<bool> HasMergeConflicts(BitbucketPullRequest pullRequest)
        {
            BitbucketMergeStatus Response = await this.bitbucketClient.TestMerge(pullRequest);

            // TODO, may need to handle vetos
            return Response.IsConflicted;
        }

        private async Task<bool> AreAllCommitsReviewed(BitbucketPullRequest pullRequest)
        {
            BitbucketPageResponse<BitbucketPullRequestCommit> Response = await this.bitbucketClient.GetCommits(pullRequest);
            FishEyeChangesets Changesets;

            if (Response.Values.Count <= 0)
            {
                return true;
            }

            Changesets = await this.fishEyeClient.GetReviewsForChangesets(pullRequest.FromReference.Repository.Slug, 
                Response.Values.Select(s => s.Id).ToList());

            List<FishEyeChangeset> ChangesetsMissingReviews = Changesets.Changesets.FindAll(c => c.Reviews.Count <= 0);

            if (ChangesetsMissingReviews.Count > 0)
            {
                // List commits that are not associated with a review
                await this.bitbucketClient.AddComment(pullRequest, new BitbucketComment()
                {
                    Text = string.Format(Resources.PullRequest_CommitsNotAssociatedWithAReview, 
                        string.Join("\n", ChangesetsMissingReviews.Select(s => $"* {s.ChangesetId}")))
                });
            }

            return false;
        }
    }
}
