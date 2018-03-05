using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketRepository
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("scmId")]
        public string Scm { get; set; }

        [JsonProperty("state")]
        public BitbucketRepositoryState State { get; set; }

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("forkable")]
        public bool IsForkable { get; set; }

        [JsonProperty("project")]
        public BitbucketProject Project { get; set; }

        [JsonProperty("public")]
        public bool IsPublic { get; set; }
    }
}
