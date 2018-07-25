using Isac.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Isac.Integrations.Atlassian.Bitbucket
{
    public static class BitbucketMvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddBitbucket(this IMvcCoreBuilder builder)
        {
            Guard.AgainstNullArgument<IMvcCoreBuilder>(nameof(builder), builder);

            BitbucketServiceCollectionSetup.AddBitbucketServices(builder.Services);

            return builder;
        }
    }
}
