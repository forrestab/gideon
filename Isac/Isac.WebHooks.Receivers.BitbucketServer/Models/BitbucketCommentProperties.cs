using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketCommentProperties
    {
        [JsonProperty("repositoryId")]
        public long RepositoryId { get; set; }
    }
}
