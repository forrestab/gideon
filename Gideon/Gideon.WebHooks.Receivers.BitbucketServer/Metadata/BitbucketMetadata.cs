using Microsoft.AspNetCore.WebHooks.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gideon.WebHooks.Receivers.BitbucketServer.Metadata
{
    public class BitbucketMetadata : WebHookMetadata, IWebHookEventMetadata, IWebHookBodyTypeMetadataService, IWebHookBindingMetadata
    {
        public WebHookBodyType BodyType => WebHookBodyType.Json;
        public string ConstantValue => null;
        public string HeaderName => BitbucketConstants.EventHeaderName;
        public string QueryParameterName => null;
        public IReadOnlyList<WebHookParameter> Parameters { get; } = new WebHookParameter[]
        {
            new WebHookParameter(BitbucketConstants.RequestIdParameterName, WebHookParameterType.Header, 
                BitbucketConstants.RequestIdHeaderName)
        };

        public BitbucketMetadata()
            : base(BitbucketConstants.ReceiverName)
        { }
    }
}
