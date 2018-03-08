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

            if (this.HasMergeConflicts())
            {
                // update the pr with needs_work
                // comment with, please fix merge conflicts

                return;
            }

            // check other criteria
        }

        private async Task AddBotAsReviewer(PullRequestOpenedNotification notification)
        {
            if (!this.IsReviewer(notification.PullRequest.Reviewers))
            {
                // add the bot as a reviewer
                await this.bitbucketClient.AddReviewer(notification.PullRequest.ToReference.Repository.Project.Key,
                    notification.PullRequest.ToReference.Repository.Slug, notification.PullRequest.Id, "gideonbot", 
                    BitbucketRole.Reviewer);
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

        private bool HasMergeConflicts()
        {
            return false;
        }
    }
}
