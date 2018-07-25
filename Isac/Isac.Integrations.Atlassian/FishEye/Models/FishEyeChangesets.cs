using Newtonsoft.Json;
using System.Collections.Generic;

namespace Isac.Integrations.Atlassian.FishEye.Models
{
    public class FishEyeChangesets
    {
        [JsonProperty("changesets")]
        public List<FishEyeChangeset> Changesets { get; set; }
    }
}
