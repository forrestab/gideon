using Isac.WebHooks.Receivers.BitbucketServer.Models.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketComment
    {
        // TODO, check if in all objects
        [JsonProperty("properties")]
        public BitbucketCommentProperties Properties { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("author")]
        public BitbucketUser Author { get; set; }

        [JsonProperty("createdDate")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset? CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset? UpdatedDate { get; set; }

        // TODO, create actual comment object
        [JsonProperty("comments")]
        public List<JObject> Comments { get; set; }

        // TODO, create actual task object
        [JsonProperty("tasks")]
        public List<JObject> Tasks { get; set; }

        // TODO, check if in all objects
        [JsonProperty("permittedOperations")]
        public BitbucketPermittedOperations PermittedOperations { get; set; }
    }
}
