using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketForkedRepository : BitbucketRepository
    {
        [JsonProperty("origin")]
        public BitbucketRepository Origin { get; set; }
    }
}
