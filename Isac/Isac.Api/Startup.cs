using Isac.Api.Extensions;
using Isac.Api.Services;
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

            services.AddBitbucketClient();
            services.AddFishEyeClient();

            services.AddScoped<IPullRequestService, PullRequestService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
