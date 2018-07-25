using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
{
    public class RepositoryCommentEditedNotification : RepositoryCommentNotification
    {
        [JsonProperty("previousComment")]
        public string PreviousComment { get; set; }
    }
}
