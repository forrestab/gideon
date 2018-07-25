using Isac.Integrations.Atlassian.FishEye.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Isac.Integrations.Atlassian.FishEye
{
    public interface IFishEyeService
    {
        Task<FishEyeChangesets> FindReviewsByCommitIds(string repositoryKey, List<string> commitIds);
    }
}
