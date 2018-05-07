using Newtonsoft.Json;
using System.Collections.Generic;

namespace Isac.Api.Models.Crucible
{
    public class CrucibleReviewers
    {
        [JsonProperty("reviewer")]
        public List<CrucibleReviewer> Reviewers { get; set; }
    }
}
