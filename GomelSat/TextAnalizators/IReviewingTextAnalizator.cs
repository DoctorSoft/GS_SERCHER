using System.Security.Cryptography.X509Certificates;

namespace TextAnalizators
{
    public interface IReviewingTextAnalizator
    {
        string GetFormattedText(string text, string title, string formattedImage, string formattedSourceLink);

        string GetShortText(string formattedText);

        string GetFormattedImageLink(string simpleImageLink);

        string GetFormattedSourceLink(string simpleSourceLink);
    }
}
