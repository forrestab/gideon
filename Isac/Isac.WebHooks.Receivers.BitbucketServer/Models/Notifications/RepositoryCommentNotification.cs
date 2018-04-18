using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class RepositoryCommentNotification : RepositoryNotification
    {
        [JsonProperty("comment")]
        public BitbucketComment Comment { get; set; }

        [JsonProperty("commit")]
        public string Commit { get; set; }
    }
}
