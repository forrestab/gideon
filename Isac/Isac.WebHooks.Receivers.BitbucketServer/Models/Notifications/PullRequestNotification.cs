using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class PullRequestNotification : BitbucketNotification
    {
        [JsonProperty("pullRequest")]
        public BitbucketPullRequest PullRequest { get; set; }
    }
}
