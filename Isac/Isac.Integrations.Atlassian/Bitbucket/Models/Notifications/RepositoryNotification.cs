using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
{
    public class RepositoryNotification : BitbucketNotification
    {
        [JsonProperty("repository")]
        public BitbucketForkedRepository Repository { get; set; }
    }
}
