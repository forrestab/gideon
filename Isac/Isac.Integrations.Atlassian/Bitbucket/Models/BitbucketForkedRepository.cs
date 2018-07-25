using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models
{
    public class BitbucketForkedRepository : BitbucketRepository
    {
        [JsonProperty("origin")]
        public BitbucketRepository Origin { get; set; }
    }
}
