using Gideon.WebHooks.Receivers.BitbucketServer.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Extensions
{
    public static class BitbucketMvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddBitbucketWebHooks(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            BitbucketServiceCollectionSetup.AddBitbucketServices(builder.Services);

            return builder
                .AddJsonFormatters()
                .AddWebHooks();
        }
    }
}
