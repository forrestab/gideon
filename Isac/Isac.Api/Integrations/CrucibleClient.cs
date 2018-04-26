using Isac.Api.Configuration;
using Isac.Api.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Isac.Api.Integrations
{
    public class CrucibleClient : ICrucibleClient
    {
        private readonly HttpClient client;

        public CrucibleClient(HttpClient client, IOptions<IntegrationsConfig> config)
        {
            this.client = client.Configure(config.Value.Crucible);
        }
    }
}
