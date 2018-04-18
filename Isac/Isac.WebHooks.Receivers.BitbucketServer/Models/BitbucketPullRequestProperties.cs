using Newtonsoft.Json;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketPullRequestProperties
    {
        [JsonProperty("mergeCommit")]
        public BitbucketMergeCommit MergeCommit { get; set; }
    }
}
