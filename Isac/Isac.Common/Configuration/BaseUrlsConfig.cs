using Isac.Common.Extensions;
using System;

namespace Isac.Common.Configuration
{
    public class BaseUrlsConfig
    {
        public Uri Base { get; set; }
        public string ApiPath { get; set; }
        public Uri Api { get { return this.Base.CombinePath(this.ApiPath); } }
    }
}
