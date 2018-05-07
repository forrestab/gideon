using System.Text.RegularExpressions;

namespace Isac.Api.Extensions
{
    public static class StringExtensions
    {
        // Looks like `string.Format` is escaping the newline character which makes it not render
        // correctly in Bitbucket.
        // https://stackoverflow.com/questions/11101359/how-to-prevent-c-sharp-from-escaping-my-string
        public static string Unescape(this string value)
        {
            if (value.Contains("\\\\n"))
            {
                return Regex.Unescape(value);
            }

            return value;
        }
    }
}
