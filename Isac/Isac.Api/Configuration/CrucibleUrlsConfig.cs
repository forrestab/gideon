using Isac.Api.Extensions;
using System;

namespace Isac.Api.Configuration
{
    public class CrucibleUrlsConfig : BaseUrlsConfig
    {
        public string ReviewPath { get; set; }
        public Uri Review { get { return base.Base.CombinePath(this.ReviewPath); } }
    }
}
