using Gideon.Api.Http;
using Gideon.WebHooks.Receivers.BitbucketServer.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gideon.Api.Services
{
    public class BitbucketClient : IBitbucketClient
    {
        private readonly HttpClient client;

        public BitbucketClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<HttpResponseMessage> AddReviewer(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer)
        {
            return await this.AddReviewer(pullRequest.ToReference.Repository.Project.Key, pullRequest.ToReference.Repository.Slug,
                pullRequest.Id, reviewer);
        }

        public async Task<HttpResponseMessage> AddReviewer(string projectKey, string repositorySlug, long pullRequestId, 
            BitbucketParticipant reviewer)
        {
            string UriPath = $"projects/{projectKey}/repos/{repositorySlug}/pull-requests/{pullRequestId}/participants";

            return await this.client.PostAsync(UriPath, new JsonContent<BitbucketParticipant>(reviewer));
        }
    }
}
