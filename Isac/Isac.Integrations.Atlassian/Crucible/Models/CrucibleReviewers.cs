using Newtonsoft.Json;
using System.Collections.Generic;

namespace Isac.Integrations.Atlassian.Crucible.Models
{
    public class CrucibleReviewers
    {
        [JsonProperty("reviewer")]
        public List<CrucibleReviewer> Reviewers { get; set; }
    }
}
