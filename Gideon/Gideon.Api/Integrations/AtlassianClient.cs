using System.Net.Http;

namespace Gideon.Api.Integrations
{
    public class AtlassianClient : IAtlassianClient
    {
        public HttpClient Client { get; }

        public AtlassianClient(HttpClient client)
        {
            this.Client = client;
        }
    }
}
