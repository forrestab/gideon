using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
{
    public class PullRequestCommentNotification : PullRequestNotification
    {
        [JsonProperty("comment")]
        public BitbucketComment Comment { get; set; }

        [JsonProperty("commentParentId")]
        public long CommentParentId { get; set; }
    }
}
