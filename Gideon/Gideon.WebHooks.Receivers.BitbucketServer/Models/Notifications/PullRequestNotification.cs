using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class PullRequestNotification : BitbucketNotification
    {
        [JsonProperty("pullRequest")]
        public BitbucketPullRequest PullRequest { get; set; }
    }
}
