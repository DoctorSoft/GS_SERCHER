using System.Collections.Generic;
using TextAnalizators.Models;

namespace TextAnalizators
{
    public interface ITextAnalizator<in TNewsContentModel>
    {
        AnalizedTextModel Analize(AnalizingTextModel model, TNewsContentModel newsContentModel, IEnumerable<string> banList);
    }
}
