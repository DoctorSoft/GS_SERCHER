using System.Collections.Generic;
using DataParsers.Models;
using TextAnalizators.Models;

namespace TextAnalizators
{
    public interface ITextAnalizator<in TNewsContentModel>
    {
        AnalizedTextModel Analize(GomelSatNewsModel newsContentModel, IEnumerable<string> wordList, IEnumerable<string> banList);

        IEnumerable<string> GetNewsWordList(AnalizingTextModel model, IEnumerable<string> banList);
    }
}
