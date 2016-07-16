using System.Collections.Generic;
using System.Linq;
using DataParsers.Helpers;
using DataParsers.Models;
using TextAnalizators.Helpers;
using TextAnalizators.Models;

namespace TextAnalizators
{
    public class GomelSatTextAnalizator : ITextAnalizator<GomelSatNewsModel>
    {
        public AnalizedTextModel Analize(AnalizingTextModel model, GomelSatNewsModel newsContentModel, IEnumerable<string> banList)
        {
            var preparedNews = " " + TagsHandleHelper.RemoveTags(TextHandleHelper.ConvertToPatternForm(newsContentModel.Text.ToLower())) + " "; 
            var preparedText = " " + TagsHandleHelper.RemoveTags(TextHandleHelper.ConvertToPatternForm(model.NewsText)).ToLower() + " ";

            char[] delimiterChars = { ' ', ',', '.', ':', '\t', ';', '-', '?', '!', '_', '(', ')', '«', '»', '\'', '\"', '/', '<', '>' };

            preparedText = preparedText.Replace("\n", TextHandleHelper.EnterPattern);
            preparedText = preparedText.Replace("\r", TextHandleHelper.EnterPattern);
            preparedText = preparedText.Replace(TextHandleHelper.EnterPattern, " ");
            var words = preparedText
                .Split(delimiterChars)
                .Distinct()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            var forbiddenWords = new List<string>{"span", "red", "color", "b", "style"};

            var wordBanList = banList.Union(forbiddenWords).ToList();

            var realWordList = words.Except(wordBanList).ToList();

            foreach (var word in realWordList)
            {
                foreach (var startDelimiterChar in delimiterChars)
                {
                    foreach (var endDelimiterChar in delimiterChars)
                    {
                        var completedWord = startDelimiterChar + word + endDelimiterChar;
                        preparedNews = preparedNews.Replace(completedWord, string.Format(" <span style=\"color: red\"><b>{0}</b></span> ", completedWord));
                    }
                }
            }

            preparedNews = TextHandleHelper.DeconvertToPatternForm(preparedNews);

            return new AnalizedTextModel
            {
                NewsHeader = newsContentModel.HeaderName,
                NewsText = preparedNews
            };
        }
    }
}
