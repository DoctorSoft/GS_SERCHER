using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DataParsers.Helpers;
using DataParsers.Models;
using TextAnalizators.Helpers;
using TextAnalizators.Models;

namespace TextAnalizators
{
    public class GomelSatTextAnalizator : ITextAnalizator<GomelSatNewsModel>
    {
        public static char[] DelimiterChars = { ' ', ',', '.', ':', '\t', ';', '-', '?', '!', '_', '(', ')', '«', '»', '\'', '\"', '/', '<', '>', '\n', '\0', '\\', '–', '[', ']', '\r' };

        public AnalizedTextModel Analize(GomelSatNewsModel newsContentModel, IEnumerable<string> wordList, IEnumerable<string> banList)
        {
            var preparedNews = " " + TextHandleHelper.ConvertToPatternForm(newsContentModel.Text.ToLower()) + " ";

            var newsContentWordList = GetNewsWordList(new AnalizingTextModel {NewsText = preparedNews}, banList);

            var list = wordList
                .Intersect(newsContentWordList)
                .Where(s => !string.IsNullOrWhiteSpace(s) && s != TextHandleHelper.EnterPattern)
                .Distinct().ToList();

            var splittedText = Regex.Split(preparedNews, "colorend", RegexOptions.IgnoreCase).ToList();
            var lastWord = splittedText.Last();

            var contentSplittedText = Regex.Split(lastWord, "<[^<]*div>[^<]*");
            var firstContentText = contentSplittedText.First();

            foreach (var word in list)
            {
                foreach (var startDelimiterChar in DelimiterChars)
                {
                    foreach (var endDelimiterChar in DelimiterChars)
                    {
                        var s = startDelimiterChar + word + endDelimiterChar;
                        firstContentText = firstContentText.Replace(s, string.Format(" {0} <span class=\"underlined-word\"> {1} </span> {2} ", startDelimiterChar, word, endDelimiterChar));
                    }
                }
            }

            contentSplittedText[0] = firstContentText;
            lastWord = string.Join("</div>", contentSplittedText);

            splittedText[splittedText.Count - 1] = lastWord;

            preparedNews = string.Join("colorend", splittedText);

            preparedNews = Regex.Replace(preparedNews, "<[^a-zA-Zа-яА-Я0-9>]*b[^a-zA-Zа-яА-Я0-9>]*>", " ");
            preparedNews = Regex.Replace(preparedNews, "style=\"[^\\\"]*#000066[^\\\"]*\\\"", "class=\"news-text-header\"");

            preparedNews = TextHandleHelper.DeconvertToPatternForm(preparedNews);

            return new AnalizedTextModel
            {
                NewsHeader = newsContentModel.HeaderName,
                NewsText = preparedNews,
                FoundWordsCount = list.Count
            };
        }

        public IEnumerable<string> GetNewsWordList(AnalizingTextModel model, IEnumerable<string> banList)
        {
            var preparedText = " " + TagsHandleHelper.RemoveTags(TextHandleHelper.ConvertToPatternForm(model.NewsText)).ToLower() + " ";

            preparedText = preparedText.Replace(" \n", TextHandleHelper.EnterPattern);
            preparedText = preparedText.Replace(" \r", TextHandleHelper.EnterPattern);
            preparedText = preparedText.Replace(TextHandleHelper.EnterPattern, " ");
            var words = preparedText
                .Split(DelimiterChars)
                .Distinct()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            var forbiddenWords = new List<string> { "span", "class", "underlined", "bword", "div", TextHandleHelper.EnterPattern };

            var wordBanList = banList.Union(forbiddenWords).ToList();

            var realWordList = words.Except(wordBanList).Distinct().ToList();

            return realWordList;
        }
    }
}
