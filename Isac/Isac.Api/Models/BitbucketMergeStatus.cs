using Isac.Api.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Isac.Api.Models
{
    public class BitbucketMergeStatus
    {
        [JsonProperty("canMerge")]
        public bool CanMerge { get; set; }

        [JsonProperty("conflicted")]
        public bool IsConflicted { get; set; }

        [JsonProperty("outcome")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketOutcome Outcome { get; set; }

        [JsonProperty("vetoes")]
        public List<BitbucketVeto> Vetoes { get; set; }
    }
}
