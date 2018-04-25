using System;
using System.Net;

namespace Isac.Api.Configuration
{
    public class ClientConfig
    {
        public Uri BaseUrl { get; set; }
        public NetworkCredential Credentials { get; set; }
        public string AccessToken { get; set; }

        public bool HasAccessToken { get { return !string.IsNullOrEmpty(this.AccessToken); } }
    }
}
