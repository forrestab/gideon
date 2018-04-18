using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class RepositoryCommentEditedNotification : RepositoryCommentNotification
    {
        [JsonProperty("previousComment")]
        public string PreviousComment { get; set; }
    }
}
