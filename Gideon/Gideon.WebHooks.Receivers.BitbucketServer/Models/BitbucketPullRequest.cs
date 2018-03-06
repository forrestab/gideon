using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketPullRequest
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        // TODO, check if in all objects
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketPullRequestState State { get; set; }

        [JsonProperty("open")]
        public bool IsOpen { get; set; }

        [JsonProperty("closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        // TODO, check if in all objects
        [JsonProperty("closedDate")]
        public DateTime ClosedDate { get; set; }

        [JsonProperty("fromRef")]
        public BitbucketReference FromReference { get; set; }

        [JsonProperty("toRef")]
        public BitbucketReference ToReference { get; set; }

        [JsonProperty("locked")]
        public bool IsLocked { get; set; }

        [JsonProperty("author")]
        public BitbucketAuthor Author { get; set; }

        [JsonProperty("reviewers")]
        public List<BitbucketParticipant> Reviewers { get; set; }

        [JsonProperty("participants")]
        public List<BitbucketParticipant> Participants { get; set; }

        [JsonProperty("links")]
        public BitbucketLinks Links { get; set; }

        // TODO, check if in all objects
        [JsonProperty("properties")]
        public BitbucketPullRequestProperties Properties { get; set; }
    }
}
