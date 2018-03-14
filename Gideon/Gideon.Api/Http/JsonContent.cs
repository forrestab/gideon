using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Gideon.Api.Http
{
    public class JsonContent<T> : StringContent
    {
        public JsonContent(T content)
            : this(content, Encoding.UTF8)
        { }

        public JsonContent(T content, Encoding encoding)
            // Looks like `string.Format` is escaping the newline character which makes it not render
            // correctly in Bitbucket.
            // https://stackoverflow.com/questions/11101359/how-to-prevent-c-sharp-from-escaping-my-string
            : base(Regex.Unescape(JsonConvert.SerializeObject(content)), encoding, "application/json")
        { }
    }
}
