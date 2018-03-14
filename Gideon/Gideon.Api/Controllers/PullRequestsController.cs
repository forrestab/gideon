using Gideon.Api.Services;
using Gideon.WebHooks.Receivers.BitbucketServer;
using Gideon.WebHooks.Receivers.BitbucketServer.Events;
using Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Gideon.Api.Controllers
{
    public class PullRequestsController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IPullRequestService pullRequestService;

        public PullRequestsController(ILoggerFactory loggerFactory, IPullRequestService pullRequestService)
        {
            this.logger = loggerFactory.CreateLogger<PullRequestsController>();
            this.pullRequestService = pullRequestService;
        }

        [BitbucketWebHook(EventName = BitbucketPullRequestEvent.OPENED)]
        public async Task<IActionResult> PullRequestOpened(string @event, string requestId, PullRequestOpenedNotification data)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            await this.pullRequestService.OnOpenedHandlerAsync(data);

            return this.Ok();
        }
    }
}
