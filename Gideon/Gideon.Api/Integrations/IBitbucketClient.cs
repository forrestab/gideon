using Gideon.Api.Models;
using Gideon.WebHooks.Receivers.BitbucketServer.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gideon.Api.Integrations
{
    public interface IBitbucketClient
    {
        Task<HttpResponseMessage> AddComment(BitbucketPullRequest pullRequest, BitbucketComment comment);
        Task<HttpResponseMessage> AddReviewer(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer);
        Task<HttpResponseMessage> SetReviewerStatus(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer);
        Task<BitbucketMergeStatus> TestMerge(BitbucketPullRequest pullRequest);
    }
}
