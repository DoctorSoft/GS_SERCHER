using System.Linq;
using System.Text.RegularExpressions;

namespace TextAnalizators
{
    public class GomelSatReviewingTextAnalizator : IReviewingTextAnalizator
    {
        public string GetFormattedText(string text, string title, string formattedImage, string formattedSourceLink)
        {
            var fomattedTextWithoutSourceLink = formattedImage + "[b][color=#000066]" + title + "[/color][/b]" + "\r\n"
                                                + text;

            fomattedTextWithoutSourceLink = Regex.Replace(fomattedTextWithoutSourceLink, "[^\r\n]([\r|\n])*$", "");

            var formattedText = fomattedTextWithoutSourceLink + "\r\n\r\n" + formattedSourceLink;

            return formattedText;
        }

        public string GetShortText(string formattedText)
        {
            var sentences = formattedText
                .Trim()
                .Replace("\r\n", " ")
                .Replace("[/b]", "[/b]\r\n")
                .Split('.')
                .ToList();

            var length = 750;
            var shortText = "";
            foreach (var sentence in sentences)
            {
                shortText += sentence + ".";
                if (shortText.Length >= length)
                {
                    break;
                }
            }

            shortText = shortText.Trim();
            shortText += "..";

            return shortText;
        }

        public string GetFormattedImageLink(string simpleImageLink)
        {
            var formattedLink = Regex.Replace(simpleImageLink, "^\\[img\\]", "[IMG=left]");
            return formattedLink;
        }

        public string GetFormattedSourceLink(string simpleSourceLink)
        {
            var urlWithoutPrefix = Regex.Replace(simpleSourceLink, "^((http|https):\\/\\/){0,1}(www\\.){0,1}", "");
            var formattedUrl = Regex.Match(urlWithoutPrefix, "^[^\\/]*").Value;

            return formattedUrl;
        }
    }
}
