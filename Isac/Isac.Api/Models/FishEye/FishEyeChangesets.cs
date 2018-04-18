using Newtonsoft.Json;
using System.Collections.Generic;

namespace Isac.Api.Models.FishEye
{
    public class FishEyeChangesets
    {
        [JsonProperty("changesets")]
        public List<FishEyeChangeset> Changesets { get; set; }
    }
}
