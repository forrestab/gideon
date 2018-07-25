using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Isac.Integrations.Atlassian.Bitbucket.Models
{
    public class BitbucketLinks
    {
        // TODO, define this
        [JsonProperty("self")]
        public List<JObject> Self { get; set; }
    }
}
