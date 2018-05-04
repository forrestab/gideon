using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isac.Api.Models
{
    public class Problem
    {
        public string Key { get; set; }
        public Uri ResourceUrl { get; set; }
        public string Message { get; set; }
    }
}
