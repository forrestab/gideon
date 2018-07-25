using Isac.Integrations.Atlassian.Bitbucket.Models;
using System.Collections.Generic;

namespace Isac.Integrations.Atlassian.Bitbucket.Extensions
{
    public static class BitbucketParticipantExtensions
    {
        public static bool HasReviewer(this List<BitbucketParticipant> reviewers, string userName)
        {
            return reviewers.Exists(reviewer =>
            {
                return reviewer.User.Name.Equals(userName);
            });
        }
    }
}
