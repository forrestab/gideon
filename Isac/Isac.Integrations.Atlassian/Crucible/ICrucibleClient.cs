using Isac.Integrations.Atlassian.Crucible.Models;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.Crucible
{
    public interface ICrucibleClient
    {
        Task<CrucibleReview> GetReviewDetails(string reviewId);
    }
}
