using System.Text.RegularExpressions;
using DataParsers.Helpers;
using DataParsers.Models;

namespace DataParsers.NewsParsers
{
    public class GomelSatNewsContentParser : ISiteNewsContentParser<GomelSatNewsContentModel>
    {
        public GomelSatNewsContentModel GetContent(GomelSatNewsContentModel pageTextModel)
        {
            var convertedText = TextHandleHelper.ConvertToPatternForm(pageTextModel.Text);

            return new GomelSatNewsContentModel
            {
                Text = TextHandleHelper.DeconvertToPatternForm(ParseMainContent(convertedText)),
                Link = pageTextModel.Link
            };
        }

        private string ParseMainContent(string text)
        {
            var contentPattern = @"<div((?!news-id|<div).)*news-id-.*<strong>Другие новости по теме:<\/strong>";
            var regex = new Regex(contentPattern);

            var textWithExtraData = regex.Match(text).Value;

            contentPattern = @"^((?!<strong>).)*";
            regex = new Regex(contentPattern);

            return regex.Match(textWithExtraData).Value;
        }
    }
}
