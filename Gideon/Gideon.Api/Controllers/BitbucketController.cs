using Gideon.WebHooks.Receivers.BitbucketServer;
using Gideon.WebHooks.Receivers.BitbucketServer.Events;
using Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Gideon.Api.Controllers
{
    public class BitbucketController : ControllerBase
    {
        private readonly ILogger logger;

        public BitbucketController(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<BitbucketController>();
        }

        [BitbucketWebHook(EventName = BitbucketPullRequestEvent.OPENED)]
        public async Task<IActionResult> PullRequestOpened(string @event, string requestId, PullRequestOpenedNotification data)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            this.logger.LogInformation("{ControllerName} received '{EventName}'.", nameof(BitbucketController), @event);

            return this.Ok();
        }

        [BitbucketWebHook]
        public async Task<IActionResult> FallbackHandler(string receiverName, string eventName, string requestId, JObject data)
        {
            return this.Ok();
        }
    }
}
