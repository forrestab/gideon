using System.Net.Http;

namespace Gideon.Api.Integrations
{
    public class FishEyeClient : IFishEyeClient
    {
        private readonly HttpClient client;

        public FishEyeClient(HttpClient client)
        {
            this.client = client;
        }
    }
}
