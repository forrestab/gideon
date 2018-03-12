using Gideon.Api.Models;
using Gideon.WebHooks.Receivers.BitbucketServer.Models;
using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gideon.Api.Services
{
    public class PullRequestService : IPullRequestService
    {
        private readonly IBitbucketClient bitbucketClient;

        public PullRequestService(IBitbucketClient bitbucketClient)
        {
            this.bitbucketClient = bitbucketClient;
        }

        public async Task OnOpenedHandlerAsync(PullRequestOpenedNotification notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            await this.AddBotAsReviewer(notification);

            if (await this.HasMergeConflicts(notification.PullRequest))
            {
                // update the pr with needs_work
                // comment with, please fix merge conflicts and add steps for fix

                return;
            }

            // check other criteria
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
                        Name = "gideonbot"
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
                return reviewer.User.Name.Equals("gideonbot");
            });
        }

        private async Task<bool> HasMergeConflicts(BitbucketPullRequest pullRequest)
        {
            BitbucketMergeStatus Response = await this.bitbucketClient.TestMerge(pullRequest);

            // TODO, may need to handle vetos
            return Response.IsConflicted;
        }
    }
}
