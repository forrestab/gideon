using Isac.Integrations.Atlassian.Crucible.Models;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.Crucible
{
    public interface ICrucibleService
    {
        Task<CrucibleReview> GetReviewDetailsAsync(string reviewId);
    }
}
