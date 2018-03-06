using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketCommentProperties
    {
        [JsonProperty("repositoryId")]
        public long RepositoryId { get; set; }
    }
}
