using Isac.Api.Integrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Isac.Api.Extensions
{
    public static class FishEyeClientBuilderExtensions
    {
        public static IHttpClientBuilder AddFishEyeClient(this IServiceCollection services)
        {
            return services.AddHttpClient<IFishEyeClient, FishEyeClient>(client =>
            {
                // TODO, pull address from configuration
                client.BaseAddress = new Uri("http://localhost:8080/rest-service-fe/");
                // TODO, add better authentication
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String(Encoding.ASCII.GetBytes("")));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
