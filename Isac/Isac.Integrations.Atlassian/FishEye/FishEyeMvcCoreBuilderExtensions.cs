using Isac.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Isac.Integrations.Atlassian.FishEye
{
    public static class FishEyeMvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddFishEye(this IMvcCoreBuilder builder)
        {
            Guard.AgainstNullArgument<IMvcCoreBuilder>(nameof(builder), builder);

            FishEyeServiceCollectionSetup.AddFishEyeServices(builder.Services);

            return builder;
        }
    }
}
