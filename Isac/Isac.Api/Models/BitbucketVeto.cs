using Newtonsoft.Json;

namespace Isac.Api.Models
{
    public class BitbucketVeto
    {
        [JsonProperty("summaryMessage")]
        public string SummaryMessage { get; set; }

        [JsonProperty("detailedMessage")]
        public string DetailedMessage { get; set; }
    }
}
