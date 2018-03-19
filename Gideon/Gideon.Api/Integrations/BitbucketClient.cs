using Gideon.Api.Http;
using Gideon.Api.Models;
using Gideon.Api.Utilities;
using Gideon.WebHooks.Receivers.BitbucketServer.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gideon.Api.Integrations
{
    public class BitbucketClient : IBitbucketClient
    {
        private readonly HttpClient client;

        public BitbucketClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<HttpResponseMessage> AddComment(BitbucketPullRequest pullRequest, BitbucketComment comment)
        {
            Guard.AgainstNullArgument<BitbucketComment>(nameof(comment), comment);

            string RequestUri = $"{this.GetBaseUri(pullRequest)}/comments";

            return await this.client.PostAsync(RequestUri, new JsonContent<BitbucketComment>(comment));
        }

        public async Task<HttpResponseMessage> AddReviewer(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer)
        {
            Guard.AgainstNullArgument<BitbucketParticipant>(nameof(reviewer), reviewer);

            string RequestUri = $"{this.GetBaseUri(pullRequest)}/participants";

            return await this.client.PostAsync(RequestUri, new JsonContent<BitbucketParticipant>(reviewer));
        }

        public async Task<HttpResponseMessage> SetReviewerStatus(BitbucketPullRequest pullRequest, BitbucketParticipant reviewer)
        {
            Guard.AgainstNullArgument<BitbucketParticipant>(nameof(reviewer), reviewer);

            string RequestUri = $"{this.GetBaseUri(pullRequest)}/participants/{reviewer.User.Slug}";

            return await this.client.PutAsync(RequestUri, new JsonContent<BitbucketParticipant>(reviewer));
        }

        public async Task<BitbucketMergeStatus> TestMerge(BitbucketPullRequest pullRequest)
        {
            string RequestUri = $"{this.GetBaseUri(pullRequest)}/merge";

            return await this.client.GetAsync<BitbucketMergeStatus>(RequestUri);
        }

        private string GetBaseUri(BitbucketPullRequest pullRequest)
        {
            Guard.AgainstNullArgument<BitbucketPullRequest>(nameof(pullRequest), pullRequest);

            return $"projects/{pullRequest.ToReference.Repository.Project.Key}/repos/{pullRequest.ToReference.Repository.Slug}/pull-requests/{pullRequest.Id}";
        }
    }
}
