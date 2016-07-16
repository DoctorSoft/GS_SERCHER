using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataParsers.Helpers;
using DataParsers.Models;

namespace DataParsers.NewsParsers
{
    public class GomelSatNewsHeadersParser : ISiteNewsHeadersParser<GomelSatNewsHeaderModel>
    {
        public IEnumerable<GomelSatNewsHeaderModel> GetPageNewsHeaders(string pageText)
        {
            var convertedText = TextHandleHelper.ConvertToPatternForm(pageText);

            var splittedNewsHeaders = GetSplittedNewsHeaders(convertedText);

            var contents = splittedNewsHeaders.Select(s => new GomelSatNewsHeaderModel
            {
                HeaderText = TextHandleHelper.DeconvertToPatternForm(ParseHeaderContentData(s)),
                Link = ParseHeaderLink(s),
                HeaderName = ParseHeaderTitle(s)
            });

            return contents;
        }

        private IEnumerable<string> GetSplittedNewsHeaders(string pageText)
        {
            var newsHeaderSplitterPattern = "post-message((?!post-message).)*";

            var regex = new Regex(newsHeaderSplitterPattern);

            var matches = regex.Matches(pageText).Cast<Match>().Select(match => match.Value).ToList();

            return matches;
        } 

        private string ParseHeaderContentData(string text)
        {
            var headerContentPattern = @"<img((?!<\/div).)*";
            var regex = new Regex(headerContentPattern);

            return regex.Match(text).Value;
        }

        private string ParseHeaderLink(string text)
        {
            var headerContentPattern = @"http:\/\/gomel-sat\.bz\/((?!\.html|>).)*\.html";
            var regex = new Regex(headerContentPattern);

            return regex.Match(text).Value;
        }

        private string ParseHeaderTitle(string text)
        {
            var headerContentPattern = @"<a((?!http).)*http:\/\/gomel-sat\.bz\/((?!\.html|>).)*\.html((?!<\/a>).)*<\/a>";
            var regex = new Regex(headerContentPattern);

            return regex.Match(text).Value;
        }
    }
}
