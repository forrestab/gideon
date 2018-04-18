using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Gideon.Api.Models.FishEye
{
    public class FishEyeReview
    {
        [JsonProperty("proejctKey")]
        public string ProjectKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("author")]
        public JObject Author { get; set; }

        [JsonProperty("moderator")]
        public JObject Moderator { get; set; }

        [JsonProperty("creator")]
        public JObject Creator { get; set; }

        [JsonProperty("permaId")]
        public JObject PermaId { get; set; }

        [JsonProperty("permaIdHistory")]
        public List<string> PermaIdHistory { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("type")]
        public string ReviewType { get; set; }

        [JsonProperty("allowReviewersToJoin")]
        public bool AllowReviewersToJoin { get; set; }

        [JsonProperty("metricsVersion")]
        public int MetricsVersion { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("jiraIssueKey")]
        public string JiraIssueKey { get; set; }
    }
}
