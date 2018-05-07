using System;

namespace Isac.Api.Extensions
{
    public static class UriExtensions
    {
        public static Uri CombinePath(this Uri uri, string path)
        {
            if (Uri.TryCreate(uri, path, out Uri CombinedPath))
            {
                return CombinedPath;
            }

            return uri;
        }
    }
}
