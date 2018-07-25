using Isac.Common.Configuration;
using Isac.Common.Net.Http;
using Isac.Integrations.Atlassian.Crucible.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.Crucible
{
    public class CrucibleClient : ICrucibleClient
    {
        private readonly HttpClient client;

        public CrucibleClient(HttpClient client, IOptions<IntegrationsConfig> config)
        {
            this.client = client.Configure<CrucibleUrlsConfig>(config.Value.Crucible);
        }

        public async Task<CrucibleReview> GetReviewDetails(string reviewId)
        {
            string RequestUri = $"reviews-v1/{reviewId}/details";

            return await this.client.GetAsync<CrucibleReview>(RequestUri);
        }
    }
}
