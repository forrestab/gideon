using Isac.Integrations.Atlassian.Bitbucket.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
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
