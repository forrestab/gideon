using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketAuthor
    {
        [JsonProperty("user")]
        public BitbucketUser User { get; set; }

        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketRole Role { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketStatus Status { get; set; }
    }
}
