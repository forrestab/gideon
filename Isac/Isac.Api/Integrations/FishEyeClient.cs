using Isac.Api.Configuration;
using Isac.Api.Http;
using Isac.Api.Models.FishEye;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Isac.Api.Integrations
{
    public class FishEyeClient : IFishEyeClient
    {
        private readonly HttpClient client;

        public FishEyeClient(HttpClient client, IOptions<IntegrationsConfig> config)
        {
            this.client = client.Configure(config.Value.FishEye);
        }

        public async Task<FishEyeChangesets> GetReviewsForChangesets(string repositoryKey, List<string> commitIds)
        {
            string RequestUri = $"/search-v1/reviewsForChangesets/{repositoryKey}";
            List<KeyValuePair<string, string>> ChangesetIds = new List<KeyValuePair<string, string>>();

            foreach (string CommitId in commitIds)
            {
                ChangesetIds.Add(new KeyValuePair<string, string>("cs", CommitId));
            }

            return await this.client.PostAsync<FishEyeChangesets>(RequestUri, new FormUrlEncodedContent(ChangesetIds));
        }
    }
}
