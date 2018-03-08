using Gideon.WebHooks.Receivers.BitbucketServer.Models;
using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gideon.Api.Models
{
    public class NewReviewer
    {
        [JsonProperty("user")]
        public BitbucketUser User { get; set; }

        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketRole Role { get; set; }
    }
}
