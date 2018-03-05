using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketAuthor
    {
        [JsonProperty("user")]
        public BitbucketUser User { get; set; }

        [JsonProperty("role")]
        public BitbucketRole Role { get; set; }

        [JsonProperty("status")]
        public BitbucketStatus Status { get; set; }
    }
}
