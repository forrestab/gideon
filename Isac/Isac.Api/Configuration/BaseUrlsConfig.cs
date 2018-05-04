using Isac.Api.Extensions;
using System;

namespace Isac.Api.Configuration
{
    public class BaseUrlsConfig
    {
        public Uri Base { get; set; }
        public string ApiPath { get; set; }
        public Uri Api { get { return this.Base.CombinePath(this.ApiPath); } }
    }
}
