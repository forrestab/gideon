using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
{
    public class PullRequestNotification : BitbucketNotification
    {
        [JsonProperty("pullRequest")]
        public BitbucketPullRequest PullRequest { get; set; }
    }
}
