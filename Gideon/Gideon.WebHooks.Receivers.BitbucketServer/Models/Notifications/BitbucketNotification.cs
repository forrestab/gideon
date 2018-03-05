using Newtonsoft.Json;
using System;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications
{
    public class BitbucketNotification
    {
        [JsonProperty("eventKey")]
        public string EventKey { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("actor")]
        public BitbucketUser Actor { get; set; }
    }
}
