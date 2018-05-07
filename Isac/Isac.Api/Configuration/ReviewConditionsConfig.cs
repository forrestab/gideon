using Isac.Api.Models.Crucible.Enums;

namespace Isac.Api.Configuration
{
    public class ReviewConditionsConfig
    {
        public CrucibleReviewState ReviewState { get; set; }
        public int ReviewerCount { get; set; }
    }
}
