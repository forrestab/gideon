using System;
using System.Net;
using System.Text;

namespace Isac.Common.Net
{
    public static class NetworkCredentialExtensions
    {
        public static string FormatForBasicAuth(this NetworkCredential credentials)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes($"{credentials.UserName}:{credentials.Password}"));
        }
    }
}
