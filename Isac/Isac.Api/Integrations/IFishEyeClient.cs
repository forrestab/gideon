using Isac.Api.Models.FishEye;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Isac.Api.Integrations
{
    public interface IFishEyeClient
    {
        Task<FishEyeChangesets> GetReviewsForChangesets(string repositoryKey, List<string> commitIds);
    }
}
