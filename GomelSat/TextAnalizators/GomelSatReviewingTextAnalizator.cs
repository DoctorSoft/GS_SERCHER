using System.Linq;

namespace TextAnalizators
{
    public class GomelSatReviewingTextAnalizator : IReviewingTextAnalizator
    {
        public string GetFormattedText(string text, string title, string formattedImage, string formattedSourceLink)
        {
            return formattedImage + "[b][color=#000066]" + title + "[/color][/b]" + "\r\n"
                   + text +
                   "\r\n\r\n" + formattedSourceLink;
        }

        public string GetShortText(string formattedText)
        {
            var sentences = formattedText
                .Trim()
                .Replace("\r\n", "")
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

            shortText += "..";

            return shortText;
        }

        public string GetFormattedImageLink(string simpleImageLink)
        {
            throw new System.NotImplementedException();
        }

        public string GetFormattedSourceLink(string simpleSourceLink)
        {
            throw new System.NotImplementedException();
        }
    }
}
