using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketMergeCommit
    {
        [JsonProperty("displayId")]
        public string DisplayId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
