using System.Net.Http;

namespace Gideon.Api.Integrations
{
    public interface IAtlassianClient
    {
        HttpClient Client { get; }
    }
}
