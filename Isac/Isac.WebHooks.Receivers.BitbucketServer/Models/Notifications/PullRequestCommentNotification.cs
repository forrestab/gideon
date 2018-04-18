using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class PullRequestCommentNotification : PullRequestNotification
    {
        [JsonProperty("comment")]
        public BitbucketComment Comment { get; set; }

        [JsonProperty("commentParentId")]
        public long CommentParentId { get; set; }
    }
}
