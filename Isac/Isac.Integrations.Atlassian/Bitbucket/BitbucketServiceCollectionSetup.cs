using Isac.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Isac.Integrations.Atlassian.Bitbucket
{
    public static class BitbucketServiceCollectionSetup
    {
        public static void AddBitbucketServices(IServiceCollection services)
        {
            Guard.AgainstNullArgument<IServiceCollection>(nameof(services), services);

            services.AddHttpClient<IBitbucketClient, BitbucketClient>();
            services.AddScoped<IBitbucketService, BitbucketService>();
        }
    }
}
