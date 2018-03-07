using System;
using System.Collections.Generic;
using System.Text;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Events
{
    public class BitbucketPullRequestEvent
    {
        public const string OPENED = "pr:opened";
        public const string APPROVED = "pr:reviewer:approved";
        public const string UNAPPROVED = "pr:reviewer:unapproved";
        public const string NEEDS_WORK = "pr:reviewer:needs_work";
        public const string MERGED = "pr:merged";
        public const string DECLINED = "pr:declined";
        public const string DELETED = "pr:deleted";
        public const string COMMENT_ADDED = "pr:comment:added";
        public const string COMMENT_EDITED = "pr:comment:edited";
        public const string COMMENT_DELETED = "pr:comment:deleted";
    }
}
