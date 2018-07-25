using Isac.Integrations.Atlassian.Bitbucket.Models.Notifications;
using Isac.WebHooks.Receivers.BitbucketServer;
using Isac.WebHooks.Receivers.BitbucketServer.Events;
using Isac.WebHooks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Isac.WebHooks.Controllers
{
    public class PullRequestWebHooksController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IPullRequestWebHooksService webHooksService;

        public PullRequestWebHooksController(ILoggerFactory loggerFactory, IPullRequestWebHooksService webHooksService)
        {
            this.logger = loggerFactory.CreateLogger<PullRequestWebHooksController>();
            this.webHooksService = webHooksService;
        }

        [BitbucketWebHook(EventName = BitbucketPullRequestEvent.OPENED)]
        public async Task<IActionResult> PullRequestOpened(string @event, string requestId, PullRequestOpenedNotification data)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            await this.webHooksService.OnOpenedAsync(data);

            return this.Ok();
        }
    }
}
