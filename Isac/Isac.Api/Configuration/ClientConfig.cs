using System;
using System.Net;

namespace Isac.Api.Configuration
{
    public class ClientConfig
    {
        public Uri BaseUrl { get; set; }
        public NetworkCredential Credentials { get; set; }
    }
}
