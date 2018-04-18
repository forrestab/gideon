using Isac.WebHooks.Receivers.BitbucketServer.Models;
using Isac.WebHooks.Receivers.BitbucketServer.Models.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Isac.Api.Models
{
    public class BitbucketPullRequestCommit
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayId")]
        public string DisplayId { get; set; }

        [JsonProperty("author")]
        public BitbucketAuthor Author { get; set; }

        [JsonProperty("authorTimestamp")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset AuthorTimestamp { get; set; }

        [JsonProperty("committer")]
        public BitbucketAuthor Committer { get; set; }

        [JsonProperty("committerTimestamp")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset CommitterTimestamp { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("parents")]
        public List<JObject> Parents { get; set; }
    }
}
