using System.Collections.Generic;

namespace DataParsers.WordParsers
{
    public interface IWordFormsDataParser
    {
        IEnumerable<string> GetWordForms(string json);
    }
}
