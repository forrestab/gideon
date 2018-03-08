using Gideon.Api.Models;
using Gideon.WebHooks.Receivers.BitbucketServer.Models;
using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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

        public async Task AddReviewer(string projectKey, string repositorySlug, long pullRequestId, string reviewerName, BitbucketRole role)
        {
            string UriFragment = $"projects/{projectKey}/repos/{repositorySlug}/pull-requests/{pullRequestId}/participants";
            string Json = JsonConvert.SerializeObject(new NewReviewer()
            {
                User = new BitbucketUser()
                {
                    Name = reviewerName
                },
                Role = role
            });

            HttpResponseMessage Response = await this.client.PostAsync(UriFragment, new StringContent(Json, Encoding.UTF8, "application/json"));
        }
    }
}
