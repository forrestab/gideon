using System.Net;

namespace Isac.Common.Configuration
{
    public class ClientConfig<T>
    {
        public T Urls { get; set; }
        public NetworkCredential Credentials { get; set; }
        public string AccessToken { get; set; }

        public bool HasAccessToken { get { return !string.IsNullOrEmpty(this.AccessToken); } }
    }
}
