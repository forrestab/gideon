using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class RepositoryCommentEditedNotification : RepositoryCommentNotification
    {
        [JsonProperty("previousComment")]
        public string PreviousComment { get; set; }
    }
}
