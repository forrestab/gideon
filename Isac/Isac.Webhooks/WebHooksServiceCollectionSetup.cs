using Isac.Common;
using Isac.WebHooks.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Isac.WebHooks
{
    public static class WebHooksServiceCollectionSetup
    {
        public static void AddWebHooksServices(IServiceCollection services)
        {
            Guard.AgainstNullArgument<IServiceCollection>(nameof(services), services);

            services.TryAddTransient<IPullRequestWebHooksService, PullRequestWebHooksService>();
        }
    }
}
