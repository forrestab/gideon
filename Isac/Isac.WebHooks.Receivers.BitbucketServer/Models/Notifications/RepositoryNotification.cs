using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class RepositoryNotification : BitbucketNotification
    {
        [JsonProperty("repository")]
        public BitbucketForkedRepository Repository { get; set; }
    }
}
