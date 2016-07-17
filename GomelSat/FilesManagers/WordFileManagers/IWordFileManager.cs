using System.Collections;
using System.Collections.Generic;

namespace FilesManagers.WordFileManagers
{
    public interface IWordFileManager
    {
        IEnumerable<string> GetWords();

        byte[] RewriteWords(IEnumerable<string> words);
    }
}
