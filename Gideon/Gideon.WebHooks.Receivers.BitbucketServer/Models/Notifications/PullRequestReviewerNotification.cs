using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class PullRequestReviewerNotification : PullRequestNotification
    {
        [JsonProperty("participant")]
        public BitbucketParticipant Participant { get; set; }

        [JsonProperty("previousStatus")]
        public BitbucketStatus PreviousStatus { get; set; }
    }
}
