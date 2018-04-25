using Isac.Api.Configuration;
using Isac.Api.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Isac.Api.Http
{
    public static class HttpClientExtensions
    {
        public static HttpClient Configure(this HttpClient client, ClientConfig config)
        {
            client.BaseAddress = config.BaseUrl;
            client.DefaultRequestHeaders.Authorization = HttpClientExtensions.GetAuthHeader(config);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri)
        {
            HttpResponseMessage Response = await client.GetAsync(requestUri);

            Response.EnsureSuccessStatusCode();

            return await Response.Content.ReadAsJsonAsync<T>();
        }

        public static async Task<T> PostAsync<T>(this HttpClient client, string requestUri, HttpContent content)
        {
            HttpResponseMessage Response = await client.PostAsync(requestUri, content);

            Response.EnsureSuccessStatusCode();

            return await Response.Content.ReadAsJsonAsync<T>();
        }

        public static async Task<T> PutAsync<T>(this HttpClient client, string requestUri, HttpContent content)
        {
            HttpResponseMessage Response = await client.PutAsync(requestUri, content);

            Response.EnsureSuccessStatusCode();

            return await Response.Content.ReadAsJsonAsync<T>();
        }

        private static AuthenticationHeaderValue GetAuthHeader(ClientConfig config)
        {
            if (config.HasAccessToken)
            {
                return new AuthenticationHeaderValue("Bearer", config.AccessToken);
            }
            else
            {
                return new AuthenticationHeaderValue("Basic", config.Credentials.FormatForBasicAuth());
            }
        }
    }
}
