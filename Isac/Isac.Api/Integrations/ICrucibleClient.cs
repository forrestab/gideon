using Isac.Api.Models.Crucible;
using System.Threading.Tasks;

namespace Isac.Api.Integrations
{
    public interface ICrucibleClient
    {
        Task<CrucibleReview> GetReviewDetails(string reviewId);
    }
}
