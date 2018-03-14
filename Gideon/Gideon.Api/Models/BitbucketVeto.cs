using Newtonsoft.Json;

namespace Gideon.Api.Models
{
    public class BitbucketVeto
    {
        [JsonProperty("summaryMessage")]
        public string SummaryMessage { get; set; }

        [JsonProperty("detailedMessage")]
        public string DetailedMessage { get; set; }
    }
}
