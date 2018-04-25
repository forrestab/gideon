using Isac.Api.Integrations;
using Isac.Api.Services;
using Isac.Api.Settings;
using Isac.WebHooks.Receivers.BitbucketServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Isac.Api
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

            services.Configure<IntegrationSettings>(this.Configuration.GetSection("Integrations"));
            services.AddHttpClient<IBitbucketClient, BitbucketClient>();
            services.AddHttpClient<IFishEyeClient, FishEyeClient>();
            services.AddScoped<IPullRequestService, PullRequestService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
