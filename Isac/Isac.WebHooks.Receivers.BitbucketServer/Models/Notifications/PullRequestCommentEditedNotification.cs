using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class PullRequestCommentEditedNotification : PullRequestCommentNotification
    {
        [JsonProperty("previousComment")]
        public string PreviousComment { get; set; }
    }
}
