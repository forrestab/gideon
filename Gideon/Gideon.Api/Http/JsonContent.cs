using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Gideon.Api.Http
{
    public class JsonContent<T> : StringContent
    {
        public JsonContent(T content)
            : this(content, Encoding.UTF8)
        { }

        public JsonContent(T content, Encoding encoding)
            : base(JsonConvert.SerializeObject(content), encoding, "application/json")
        { }
    }
}
