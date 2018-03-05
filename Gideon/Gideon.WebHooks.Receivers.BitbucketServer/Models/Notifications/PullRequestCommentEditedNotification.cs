using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class PullRequestCommentEditedNotification : PullRequestCommentNotification
    {
        [JsonProperty("previousComment")]
        public string PreviousComment { get; set; }
    }
}
