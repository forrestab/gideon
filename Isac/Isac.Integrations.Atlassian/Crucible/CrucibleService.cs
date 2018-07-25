using Isac.Integrations.Atlassian.Crucible.Models;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.Crucible
{
    public class CrucibleService : ICrucibleService
    {
        private readonly ICrucibleClient client;

        public CrucibleService(ICrucibleClient client)
        {
            this.client = client;
        }

        public async Task<CrucibleReview> GetReviewDetailsAsync(string reviewId)
        {
            return await this.client.GetReviewDetails(reviewId);
        }
    }
}
