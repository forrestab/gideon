using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gideon.Api.Controllers
{
    [Route("api/[controller]")]
    public class BitbucketController : Controller
    {
        private readonly ILogger logger;

        public BitbucketController(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<BitbucketController>();
        }
    }
}
