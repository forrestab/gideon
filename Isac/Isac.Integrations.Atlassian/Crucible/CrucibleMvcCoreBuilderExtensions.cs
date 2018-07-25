using Isac.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Isac.Integrations.Atlassian.Crucible
{
    public static class CrucibleMvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddCrucible(this IMvcCoreBuilder builder)
        {
            Guard.AgainstNullArgument<IMvcCoreBuilder>(nameof(builder), builder);

            CrucibleServiceCollectionSetup.AddCrucibleServices(builder.Services);

            return builder;
        }
    }
}
