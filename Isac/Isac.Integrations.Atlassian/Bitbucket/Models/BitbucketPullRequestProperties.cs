using Newtonsoft.Json;

namespace Isac.Integrations.Atlassian.Bitbucket.Models
{
    public class BitbucketPullRequestProperties
    {
        [JsonProperty("mergeCommit")]
        public BitbucketMergeCommit MergeCommit { get; set; }
    }
}
