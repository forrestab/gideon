using Microsoft.AspNetCore.WebHooks.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Metadata
{
    public class BitbucketMetadata : WebHookMetadata, IWebHookBodyTypeMetadataService, IWebHookEventMetadata
    {
        public WebHookBodyType BodyType => WebHookBodyType.Json;
        public string ConstantValue => null;
        public string HeaderName => BitbucketConstants.EventHeaderName;
        public string QueryParameterName => null;

        public BitbucketMetadata()
            : base(BitbucketConstants.ReceiverName)
        { }
    }
}
