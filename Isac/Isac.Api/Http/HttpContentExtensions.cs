using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Isac.Api.Http
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string ResponseBody = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(ResponseBody);
        }
    }
}
