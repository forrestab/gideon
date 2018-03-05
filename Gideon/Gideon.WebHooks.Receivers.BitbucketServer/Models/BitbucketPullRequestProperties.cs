using Newtonsoft.Json;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Models
{
    public class BitbucketPullRequestProperties
    {
        [JsonProperty("mergeCommit")]
        public BitbucketMergeCommit MergeCommit { get; set; }
    }
}
