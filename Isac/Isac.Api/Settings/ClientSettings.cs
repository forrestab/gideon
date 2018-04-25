using System;

namespace Isac.Api.Settings
{
    public class ClientSettings
    {
        public Uri BaseUrl { get; set; }
        public CredentialSettings Credentials { get; set; }
    }
}
