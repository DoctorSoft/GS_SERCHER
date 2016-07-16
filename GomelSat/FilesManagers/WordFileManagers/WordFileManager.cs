using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using FilesManagers.Constants;

namespace FilesManagers.WordFileManagers
{
    public class WordFileManager : IWordFileManager
    {
        private readonly string path;

        public WordFileManager()
        {
            var startupPath = "C:/Temp/GSWords";
            path = Path.Combine(startupPath, FileNameConstants.WordsFileName);
        }

        public IEnumerable<string> GetWords()
        {
            using (var reader = new StreamReader(path, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        yield return line;
                    }
                }
                reader.Close();
            }
        }

        public void RewriteWords(IEnumerable<string> words)
        {
            Directory.CreateDirectory(Directory.GetDirectoryRoot(path));
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (var word in words)
                {
                    writer.WriteLine(word);
                }
                writer.Close();
            }
        }
    }
}
