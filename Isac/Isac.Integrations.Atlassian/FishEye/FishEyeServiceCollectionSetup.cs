using Isac.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Isac.Integrations.Atlassian.FishEye
{
    public static class FishEyeServiceCollectionSetup
    {
        public static void AddFishEyeServices(IServiceCollection services)
        {
            Guard.AgainstNullArgument<IServiceCollection>(nameof(services), services);

            services.AddHttpClient<IFishEyeClient, FishEyeClient>();
            services.AddScoped<IFishEyeService, FishEyeService>();
        }
    }
}
