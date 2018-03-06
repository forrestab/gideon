using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class PullRequestCommentNotification : PullRequestNotification
    {
        [JsonProperty("comment")]
        public BitbucketComment Comment { get; set; }

        [JsonProperty("commentParentId")]
        public long CommentParentId { get; set; }
    }
}
