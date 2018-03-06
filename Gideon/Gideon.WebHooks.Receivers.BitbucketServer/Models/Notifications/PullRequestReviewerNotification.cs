using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class PullRequestReviewerNotification : PullRequestNotification
    {
        [JsonProperty("participant")]
        public BitbucketParticipant Participant { get; set; }

        [JsonProperty("previousStatus")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketStatus PreviousStatus { get; set; }
    }
}
