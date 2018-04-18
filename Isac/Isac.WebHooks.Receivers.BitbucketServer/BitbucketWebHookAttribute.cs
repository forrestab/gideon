using Isac.WebHooks.Receivers.BitbucketServer.Properties;
using Microsoft.AspNetCore.WebHooks;
using Microsoft.AspNetCore.WebHooks.Metadata;
using System;

namespace Isac.WebHooks.Receivers.BitbucketServer
{
    public class BitbucketWebHookAttribute : WebHookAttribute, IWebHookEventSelectorMetadata
    {
        private string eventName;

        public string EventName
        {
            get { return this.eventName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(Resources.General_ArgumentCannotBeNullOrEmpty, nameof(value));
                }

                this.eventName = value;
            }
        }

        public BitbucketWebHookAttribute()
            : base(BitbucketConstants.ReceiverName)
        { }
    }
}
