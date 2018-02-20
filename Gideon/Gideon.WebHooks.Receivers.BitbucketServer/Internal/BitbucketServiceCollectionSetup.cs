using Gideon.WebHooks.Receivers.BitbucketServer.Metadata;
using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Internal
{
    public static class BitbucketServiceCollectionSetup
    {
        public static void AddBitbucketServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IWebHookMetadata, BitbucketMetadata>());
        }
    }
}
