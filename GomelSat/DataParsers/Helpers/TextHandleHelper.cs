using System.Text.RegularExpressions;

namespace DataParsers.Helpers
{
    public static class TextHandleHelper
    {
        public const string EnterPattern = "[[Enter]]";

        public static string ConvertToPatternForm(string text)
        {
            var convertedString = text.Replace("\n", EnterPattern);
            return convertedString;
        }

        public static string DeconvertToPatternForm(string text)
        {
            var deconvertedString = text.Replace(EnterPattern, "\n");
            return deconvertedString;
        }

        public static string GetOnlyAlphanumericWordData(string text)
        {
            var regex = new Regex("[^\\w0-9]");
            var result = regex.Replace(text, "");
            return result.ToLower();
        }
    }
}
