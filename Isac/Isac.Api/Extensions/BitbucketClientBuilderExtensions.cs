using Isac.Api.Integrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Isac.Api.Extensions
{
    public static class BitbucketClientBuilderExtensions
    {
        public static IHttpClientBuilder AddBitbucketClient(this IServiceCollection services)
        {
            return services.AddHttpClient<IBitbucketClient, BitbucketClient>(client =>
            {
                // TODO, pull address from configuration
                client.BaseAddress = new Uri("http://localhost:7990/rest/api/1.0/");
                // TODO, add better authentication
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String(Encoding.ASCII.GetBytes("")));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
