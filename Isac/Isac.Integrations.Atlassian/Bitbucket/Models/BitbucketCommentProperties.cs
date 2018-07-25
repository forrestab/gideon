using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models
{
    public class BitbucketCommentProperties
    {
        [JsonProperty("repositoryId")]
        public long RepositoryId { get; set; }
    }
}
