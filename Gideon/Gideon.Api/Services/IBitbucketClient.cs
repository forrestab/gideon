using Gideon.Api.Models;
using Gideon.WebHooks.Receivers.BitbucketServer.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gideon.Api.Services
{
    public interface IBitbucketClient
    {
        Task<HttpResponseMessage> AddComent(BitbucketPullRequest pullRequest, BitbucketComment comment);
        Task<HttpResponseMessage> AddComent(string projectKey, string repositorySlug, long pullRequestId,
            BitbucketComment comment);
        Task<HttpResponseMessage> AddReviewer(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer);
        Task<HttpResponseMessage> AddReviewer(string projectKey, string repositorySlug, long pullRequestId, 
            BitbucketParticipant reviewer);
        Task<HttpResponseMessage> SetReviewerStatus(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer);
        Task<HttpResponseMessage> SetReviewerStatus(string projectKey, string repositorySlug, long pullRequestId,
            BitbucketParticipant reviewer);
        Task<BitbucketMergeStatus> TestMerge(BitbucketPullRequest pullRequest);
        Task<BitbucketMergeStatus> TestMerge(string projectKey, string repositorySlug, long pullRequestId);
    }
}
