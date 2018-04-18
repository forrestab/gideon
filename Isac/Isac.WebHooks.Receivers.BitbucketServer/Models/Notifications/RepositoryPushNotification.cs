using Newtonsoft.Json;
using System.Collections.Generic;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class RepositoryPushNotification : RepositoryNotification
    {
        [JsonProperty("changes")]
        public List<BitbucketCommit> Commits { get; set; }
    }
}
