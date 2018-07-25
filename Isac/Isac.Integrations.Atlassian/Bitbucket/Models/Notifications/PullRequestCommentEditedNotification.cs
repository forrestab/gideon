using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
{
    public class PullRequestCommentEditedNotification : PullRequestCommentNotification
    {
        [JsonProperty("previousComment")]
        public string PreviousComment { get; set; }
    }
}
