using Isac.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Isac.Integrations.Atlassian.Crucible
{
    public static class CrucibleServiceCollectionSetup
    {
        public static void AddCrucibleServices(IServiceCollection services)
        {
            Guard.AgainstNullArgument<IServiceCollection>(nameof(services), services);

            services.AddHttpClient<ICrucibleClient, CrucibleClient>();
            services.AddScoped<ICrucibleService, CrucibleService>();
        }
    }
}
