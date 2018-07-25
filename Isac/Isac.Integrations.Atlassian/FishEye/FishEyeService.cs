using Isac.Integrations.Atlassian.FishEye.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.FishEye
{
    public class FishEyeService : IFishEyeService
    {
        private readonly IFishEyeClient client;

        public FishEyeService(IFishEyeClient client)
        {
            this.client = client;
        }

        public Task<FishEyeChangesets> FindReviewsByCommitIds(string repositoryKey, List<string> commitIds)
        {
            return this.client.GetReviewsForChangesets(repositoryKey, commitIds);
        }
    }
}
