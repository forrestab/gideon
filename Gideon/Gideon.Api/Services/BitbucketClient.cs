using Gideon.Api.Extensions;
using Gideon.Api.Http;
using Gideon.Api.Models;
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

        public async Task<BitbucketMergeStatus> TestMerge(BitbucketPullRequest pullRequest)
        {
            return await this.TestMerge(pullRequest.ToReference.Repository.Project.Key, pullRequest.ToReference.Repository.Slug,
                pullRequest.Id);
        }

        public async Task<BitbucketMergeStatus> TestMerge(string projectKey, string repositorySlug, long pullRequestId)
        {
            string UriPath = $"projects/{projectKey}/repos/{repositorySlug}/pull-requests/{pullRequestId}/merge";
            HttpResponseMessage Response = await this.client.GetAsync(UriPath);
            string Content = string.Empty;

            Response.EnsureSuccessStatusCode();
            Content = await Response.Content.ReadAsStringAsync();

            return await Content.ParseJson<BitbucketMergeStatus>();
        }
    }
}
