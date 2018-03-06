using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketParticipant
    {
        [JsonProperty("user")]
        public BitbucketUser User { get; set; }

        [JsonProperty("lastReviewedCommit")]
        public string LastReviewedCommit { get; set; }

        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketRole Role { get; set; }

        [JsonProperty("approved")]
        public bool IsApproved { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketStatus Status { get; set; }
    }
}
