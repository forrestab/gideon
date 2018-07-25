using Isac.Integrations.Atlassian.Crucible.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;

namespace Isac.Integrations.Atlassian.Crucible.Models
{
    public class CrucibleReview
    {
        [JsonProperty("projectKey")]
        public string ProjectKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public JObject Author { get; set; }

        [JsonProperty("moderator")]
        public JObject Moderator { get; set; }

        [JsonProperty("creator")]
        public JObject Creator { get; set; }

        [JsonProperty("permaId")]
        public JObject PermanentId { get; set; }

        [JsonProperty("permaIdHistory")]
        public JArray PermanentIdHistory { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CrucibleReviewState State { get; set; }

        [JsonProperty("type")]
        public string ReviewType { get; set; }

        [JsonProperty("allowReviewersToJoin")]
        public bool AllowReviewersToJoin { get; set; }

        [JsonProperty("metricsVersion")]
        public int MetricsVersion { get; set; }

        [JsonProperty("createDate")]
        public DateTimeOffset CreateDate { get; set; }

        [JsonProperty("closeDate")]
        public DateTimeOffset CloseDate { get; set; }

        [JsonProperty("dueDate")]
        public DateTimeOffset DueDate { get; set; }

        [JsonProperty("reviewers")]
        public CrucibleReviewers Reviewers { get; set; }

        [JsonProperty("reviewItems")]
        public JObject ReviewItems { get; set; }

        [JsonProperty("generalComments")]
        public JObject GeneralComments { get; set; }

        [JsonProperty("versionedComments")]
        public JObject VersionedComments { get; set; }

        [JsonProperty("transitions")]
        public JObject transitions { get; set; }

        [JsonProperty("actions")]
        public JObject Actions { get; set; }

        [JsonProperty("stats")]
        public JArray Stats { get; set; }
    }
}
