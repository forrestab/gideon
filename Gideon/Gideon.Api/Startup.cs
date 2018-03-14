using Gideon.Api.Integrations;
using Gideon.Api.Services;
using Gideon.WebHooks.Receivers.BitbucketServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Gideon.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddBitbucketWebHooks();

            services.AddHttpClient<IAtlassianClient, AtlassianClient>(client =>
            {
                // TODO, pull address from configuration
                client.BaseAddress = new Uri("http://localhost:7990/rest/api/1.0/");
                // TODO, add better authentication
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("")));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddTransient<IBitbucketClient, BitbucketClient>();
            services.AddScoped<IPullRequestService, PullRequestService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
