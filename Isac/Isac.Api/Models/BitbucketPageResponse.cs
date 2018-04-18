using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Isac.Api.Models
{
    public class BitbucketPageResponse<T>
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("isLastPage")]
        public bool IsLastPage { get; set; }

        [JsonProperty("values")]
        public List<T> Values { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("filter")]
        public JObject Filter { get; set; }

        [JsonProperty("nextPageStart")]
        public int? NextPageStart { get; set; }
    }
}
