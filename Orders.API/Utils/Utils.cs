using System.Linq;

namespace Orders.API.Utils
{
    public static class StringExtension
    {
        public static bool IsMatch(string strValue, string specialChars)
        {
            return specialChars.Any(x => strValue.Contains(x));
        }
    }
}
