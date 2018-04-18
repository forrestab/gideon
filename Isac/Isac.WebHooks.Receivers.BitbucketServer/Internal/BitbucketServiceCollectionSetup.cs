using Isac.WebHooks.Receivers.BitbucketServer.Filters;
using Isac.WebHooks.Receivers.BitbucketServer.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebHooks.Filters;
using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Isac.WebHooks.Receivers.BitbucketServer.Internal
{
    public static class BitbucketServiceCollectionSetup
    {
        public static void AddBitbucketServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcOptionsSetup>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IWebHookMetadata, BitbucketMetadata>());
            services.TryAddSingleton<BitbucketVerifySignatureFilter>();
        }
        
        private class MvcOptionsSetup : IConfigureOptions<MvcOptions>
        {
            public void Configure(MvcOptions options)
            {
                if (options == null)
                {
                    throw new ArgumentNullException(nameof(options));
                }

                options.Filters.AddService<BitbucketVerifySignatureFilter>(WebHookSecurityFilter.Order);
            }
        }
    }
}
