using System.Runtime.Serialization;

namespace Isac.Integrations.Atlassian.Bitbucket.Models.Enums
{
    public enum BitbucketStatus
    {
        Approved,
        [EnumMember(Value = "NEEDS_WORK")]
        NeedsWork,
        Unapproved
    }
}
