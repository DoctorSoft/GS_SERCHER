using System.Collections;
using System.Collections.Generic;

namespace FilesManagers.WordFileManagers
{
    public interface IWordFileManager
    {
        IEnumerable<string> GetWords();

        void RewriteWords(IEnumerable<string> words);
    }
}
