using Isac.Integrations.Atlassian.Bitbucket.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Isac.Integrations.Atlassian.Bitbucket.Models
{
    public class BitbucketProject
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        // TODO, check if in all objects
        [JsonProperty("public")]
        public bool IsPublic { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BitbucketProjectType ProjectType { get; set; }

        // TODO, check if in all objects
        [JsonProperty("owner")]
        public BitbucketUser Owner { get; set; }
    }
}
