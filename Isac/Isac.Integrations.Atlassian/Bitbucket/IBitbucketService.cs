using Isac.Integrations.Atlassian.Bitbucket.Models;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.Bitbucket
{
    public interface IBitbucketService
    {
        Task<bool> CheckForMergeConflictsAsync(BitbucketPullRequest pullRequest);
        Task<BitbucketPageResponse<BitbucketPullRequestCommit>> GetCommitsAsync(BitbucketPullRequest pullRequest);
        Task AddReviewerAsync(BitbucketPullRequest pullRequest, string userName);        
        Task SetReviewerStatusAsync(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer);
        Task AddCommentAsync(BitbucketPullRequest pullRequest, BitbucketComment comment);
        Task SetReviewerStatusAndCommentAsync(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer,
            BitbucketComment comment);
    }
}
