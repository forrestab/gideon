using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
{
    public class RepositoryModifiedNotification : BitbucketNotification
    {
        [JsonProperty("old")]
        public BitbucketRepository Old { get; set; }

        [JsonProperty("new")]
        public BitbucketRepository New { get; set; }
    }
}
