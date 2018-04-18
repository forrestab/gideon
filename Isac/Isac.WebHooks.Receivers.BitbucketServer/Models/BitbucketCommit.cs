using Isac.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketCommit
    {
        [JsonProperty("ref")]
        public BitbucketReference Reference { get; set; }

        [JsonProperty("refId")]
        public string ReferenceId { get; set; }

        [JsonProperty("fromHash")]
        public string FromHash { get; set; }

        [JsonProperty("toHash")]
        public string ToHash { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketCommitType CommitType { get; set; }
    }
}
