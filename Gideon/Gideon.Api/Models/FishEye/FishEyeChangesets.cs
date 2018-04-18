using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gideon.Api.Models.FishEye
{
    public class FishEyeChangesets
    {
        [JsonProperty("changesets")]
        public List<FishEyeChangeset> Changesets { get; set; }
    }
}
