using CommandsAndQueries.QueriesAndHandlers.Words;
using DataBase.Contexts;
using DataParsers;
using DataParsers.NewsParsers;
using DataProviders;
using DataProviders.SiteDataPrividers;
using FilesManagers.WordFileManagers;

namespace Laboratory
{
    public class Program
    {
        static void Main(string[] args)
        {
            var wordsHandler = new GetWordListQueryHandler(new DataBaseContext());
            var words = wordsHandler.Handle(new GetWordListQuery());

            var fileManager = new WordFileManager();
            fileManager.RewriteWords(words);
        }
    }
}
