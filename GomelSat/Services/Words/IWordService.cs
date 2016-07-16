using System.Collections.Generic;
using Services.Words.Models;

namespace Services.Words
{
    public interface IWordService
    {
        IEnumerable<string> GetWordList();

        void AddWord(string word);

        WordListToDeleteViewModel GetWordListToDelete();

        void DeleteWords(IEnumerable<string> words);
    }
}
