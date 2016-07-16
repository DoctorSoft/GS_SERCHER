using System.Collections.Generic;

namespace DataProviders.WordsDataProviders
{
    public interface IWordFormsProvider
    {
        string GetWordFormsData(string word);
    }
}
