using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class RepositoryNotification : BitbucketNotification
    {
        [JsonProperty("repository")]
        public BitbucketForkedRepository Repository { get; set; }
    }
}
