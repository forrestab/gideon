using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketPermittedOperations
    {
        [JsonProperty("editable")]
        public bool IsEditable { get; set; }

        [JsonProperty("deletable")]
        public bool IsDeletable { get; set; }
    }
}
