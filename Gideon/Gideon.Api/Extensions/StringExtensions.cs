using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Gideon.Api.Extensions
{
    public static class StringExtensions
    {
        public static Task<T> ParseJson<T>(this string json)
        {
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()));
        }
    }
}
