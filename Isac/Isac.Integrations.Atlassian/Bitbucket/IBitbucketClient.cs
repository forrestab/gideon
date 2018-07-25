using Isac.Integrations.Atlassian.Bitbucket.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.Bitbucket
{
    public interface IBitbucketClient
    {
        Task<HttpResponseMessage> AddComment(BitbucketPullRequest pullRequest, BitbucketComment comment);
        Task<HttpResponseMessage> AddReviewer(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer);
        Task<BitbucketPageResponse<BitbucketPullRequestCommit>> GetCommits(BitbucketPullRequest pullRequest);
        Task<HttpResponseMessage> SetReviewerStatus(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer);
        Task<BitbucketMergeStatus> TestMerge(BitbucketPullRequest pullRequest);
    }
}
