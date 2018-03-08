using Gideon.WebHooks.Receivers.BitbucketServer.Models.Enums;
using System.Threading.Tasks;

namespace Gideon.Api.Services
{
    public interface IBitbucketClient
    {
        Task AddReviewer(string projectKey, string repositorySlug, long pullRequestId, string reviewerName, BitbucketRole role);
    }
}
