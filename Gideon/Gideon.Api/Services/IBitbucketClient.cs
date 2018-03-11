using Gideon.WebHooks.Receivers.BitbucketServer.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gideon.Api.Services
{
    public interface IBitbucketClient
    {
        Task<HttpResponseMessage> AddReviewer(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer);
        Task<HttpResponseMessage> AddReviewer(string projectKey, string repositorySlug, long pullRequestId, 
            BitbucketParticipant reviewer);
    }
}
