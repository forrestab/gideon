using Isac.Common.Extensions;
using System;

namespace Isac.Common.Configuration
{
    public class CrucibleUrlsConfig : BaseUrlsConfig
    {
        public string ReviewPath { get; set; }
        public Uri Review { get { return base.Base.CombinePath(this.ReviewPath); } }
    }
}
