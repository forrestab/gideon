using Newtonsoft.Json;
using System.Collections.Generic;

namespace Isac.Integrations.Atlassian.FishEye.Models
{
    public class FishEyeChangeset
    {
        [JsonProperty("changesetId")]
        public string ChangesetId { get; set; }

        [JsonProperty("reviews")]
        public List<FishEyeReview> Reviews { get; set; }
    }
}
