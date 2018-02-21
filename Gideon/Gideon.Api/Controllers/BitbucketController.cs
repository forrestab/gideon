using Gideon.WebHooks.Receivers.BitbucketServer;
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

        [BitbucketWebHook(EventName = "pr:opened")]
        public async Task<IActionResult> PullRequestOpened(string @event, JObject data)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            this.logger.LogInformation("{ControllerName} received '{EventName}'.", nameof(BitbucketController), @event);

            return this.Ok();
        }

        [BitbucketWebHook]
        public async Task<IActionResult> FallbackHandler(string receiverName, string id, string eventName, JObject data)
        {
            return this.Ok();
        }
    }
}
