using Newtonsoft.Json;
using System;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Notifications
{
    public class BitbucketNotification
    {
        [JsonProperty("eventKey")]
        public string EventKey { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("actor")]
        public BitbucketUser Actor { get; set; }
    }
}
