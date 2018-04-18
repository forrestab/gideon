using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class RepositoryModifiedNotification : BitbucketNotification
    {
        [JsonProperty("old")]
        public BitbucketRepository Old { get; set; }

        [JsonProperty("new")]
        public BitbucketRepository New { get; set; }
    }
}
