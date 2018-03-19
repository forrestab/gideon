using Gideon.Api.Extensions;
using Gideon.Api.Integrations;
using Gideon.Api.Services;
using Gideon.WebHooks.Receivers.BitbucketServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddBitbucketClient();
            services.AddFishEyeClient();

            services.AddTransient<IBitbucketClient, BitbucketClient>();
            services.AddScoped<IPullRequestService, PullRequestService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
