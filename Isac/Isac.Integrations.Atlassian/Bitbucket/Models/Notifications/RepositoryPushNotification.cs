using Newtonsoft.Json;
using System.Collections.Generic;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
{
    public class RepositoryPushNotification : RepositoryNotification
    {
        [JsonProperty("changes")]
        public List<BitbucketCommit> Commits { get; set; }
    }
}
