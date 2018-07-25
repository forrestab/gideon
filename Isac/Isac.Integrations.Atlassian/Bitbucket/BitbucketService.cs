using Isac.Integrations.Atlassian.Bitbucket.Extensions;
using Isac.Integrations.Atlassian.Bitbucket.Models;
using Isac.Integrations.Atlassian.Bitbucket.Models.Enums;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.Bitbucket
{
    public class BitbucketService : IBitbucketService
    {
        private readonly IBitbucketClient client;

        public BitbucketService(IBitbucketClient client)
        {
            this.client = client;
        }

        public async Task<bool> CheckForMergeConflictsAsync(BitbucketPullRequest pullRequest)
        {
            BitbucketMergeStatus Response = await this.client.TestMerge(pullRequest);

            // TODO, may need to handle vetos
            return Response.IsConflicted;
        }

        public async Task<BitbucketPageResponse<BitbucketPullRequestCommit>> GetCommitsAsync(BitbucketPullRequest pullRequest)
        {
            return await this.client.GetCommits(pullRequest);
        }

        public async Task AddReviewerAsync(BitbucketPullRequest pullRequest, string userName)
        {
            if (!pullRequest.Reviewers.HasReviewer(userName))
            {
                await this.client.AddReviewer(pullRequest, new BitbucketParticipant()
                {
                    User = new BitbucketUser()
                    {
                        Name = userName
                    },
                    Role = BitbucketRole.Reviewer
                });
            }
        }

        public async Task SetReviewerStatusAsync(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer)
        {
            if (pullRequest.Reviewers.HasReviewer(reviewer.User.Name))
            {
                await this.client.SetReviewerStatus(pullRequest, reviewer);
            }
        }

        public async Task AddCommentAsync(BitbucketPullRequest pullRequest, BitbucketComment comment)
        {
            await this.client.AddComment(pullRequest, comment);
        }

        public async Task SetReviewerStatusAndCommentAsync(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer,
            BitbucketComment comment)
        {
            Task SetReviewerStatusTask = this.SetReviewerStatusAsync(pullRequest, reviewer);
            Task AddCommentTask = this.AddCommentAsync(pullRequest, comment);

            await Task.WhenAll(SetReviewerStatusTask, AddCommentTask);
        }
    }
}
