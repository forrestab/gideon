using System.Runtime.Serialization;

namespace Isac.WebHooks.Receivers.BitbucketServer.Models.Enums
{
    public enum BitbucketStatus
    {
        Approved,
        [EnumMember(Value = "NEEDS_WORK")]
        NeedsWork,
        Unapproved
    }
}
