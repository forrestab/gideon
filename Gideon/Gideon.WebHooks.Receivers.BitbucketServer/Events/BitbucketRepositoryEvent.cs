using System;
using System.Collections.Generic;
using System.Text;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Events
{
    public class BitbucketRepositoryEvent
    {
        public const string PUSH = "repo:refs_changed";
        public const string MODIFIED = "repo:modified";
        public const string FORK = "repo:fork";
        public const string COMMENT_ADDED = "repo:comment:added";
        public const string COMMENT_EDITED = "repo:comment:edited";
        public const string COMMENT_DELETED = "repo:comment:deleted";
    }
}
