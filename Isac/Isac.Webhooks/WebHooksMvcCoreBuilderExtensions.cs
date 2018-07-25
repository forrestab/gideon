using Isac.Common;
using Isac.Integrations.Atlassian.Bitbucket;
using Isac.Integrations.Atlassian.Crucible;
using Isac.Integrations.Atlassian.FishEye;
using Isac.WebHooks.Receivers.BitbucketServer.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Isac.WebHooks
{
    public static class WebHooksMvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddIsac(this IMvcCoreBuilder builder)
        {
            Guard.AgainstNullArgument<IMvcCoreBuilder>(nameof(builder), builder);

            WebHooksServiceCollectionSetup.AddWebHooksServices(builder.Services);

            return builder
                .AddBitbucketWebHooks()
                .AddBitbucket()
                .AddCrucible()
                .AddFishEye();
        }
    }
}
