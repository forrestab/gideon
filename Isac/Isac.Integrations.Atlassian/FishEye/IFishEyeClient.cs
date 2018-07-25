using Isac.Integrations.Atlassian.FishEye.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.FishEye
{
    public interface IFishEyeClient
    {
        Task<FishEyeChangesets> GetReviewsForChangesets(string repositoryKey, List<string> commitIds);
    }
}
