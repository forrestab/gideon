using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketForkedRepository : BitbucketRepository
    {
        [JsonProperty("origin")]
        public BitbucketRepository Origin { get; set; }
    }
}
