using Isac.Api.Configuration;
using Isac.Api.Http;
using Isac.Api.Models.Crucible;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace Isac.Api.Integrations
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
