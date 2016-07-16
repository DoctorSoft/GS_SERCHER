using System.Collections.Generic;
using System.Linq;
using CommandsAndQueries.CommandsAndHandlers.Words;
using CommandsAndQueries.QueriesAndHandlers.Words;
using Common.CommandQueryTools;
using Common.Constants;
using DataParsers.Helpers;
using DataParsers.WordParsers;
using DataProviders.WordsDataProviders;
using Services.Words.Models;

namespace Services.Words
{
    public class WordService : IWordService
    {
        private readonly IQueryDispatcher queryDispatcher;

        private readonly ICommandDispatcher commandDispatcher;

        private readonly IWordFormsDataParser wordFormsDataParser;

        private readonly IWordFormsProvider wordFormsProvider;

        public WordService(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IWordFormsDataParser wordFormsDataParser, IWordFormsProvider wordFormsProvider)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
            this.wordFormsDataParser = wordFormsDataParser;
            this.wordFormsProvider = wordFormsProvider;
        }

        public IEnumerable<string> GetWordList()
        {
            var getWordListQuery = new GetWordListQuery();
            var wordList = queryDispatcher.Dispatch<GetWordListQuery, IEnumerable<string>>(getWordListQuery);

            return wordList.ToList();
        }

        public void AddWord(string word)
        {
            var realWord = TextHandleHelper.GetOnlyAlphanumericWordData(word);

            var wordCanBeAddedQuery = new WordCanBeAddedQuery { Word = realWord };
            var canStartAdd = queryDispatcher.Dispatch<WordCanBeAddedQuery, bool>(wordCanBeAddedQuery);

            if (canStartAdd)
            {
                var wordFormsData = wordFormsProvider.GetWordFormsData(realWord);
                var wordForms = wordFormsDataParser.GetWordForms(wordFormsData);

                var addWordCommand = new AddWordCommand { Word = realWord };
                commandDispatcher.Dispatch<AddWordCommand, VoidCommandResponse>(addWordCommand);

                foreach (var wordForm in wordForms)
                {
                    addWordCommand = new AddWordCommand { Word = wordForm };
                    commandDispatcher.Dispatch<AddWordCommand, VoidCommandResponse>(addWordCommand);
                }
            }
        }

        public WordListToDeleteViewModel GetWordListToDelete()
        {
            var count = GetWordList().Count();

            var getLastAddedWordListQuery = new GetLastAddedWordListQuery { Count = WordConstants.WordCountAvailableToDelete };
            var words = queryDispatcher.Dispatch<GetLastAddedWordListQuery, IEnumerable<string>>(getLastAddedWordListQuery).ToList();

            return new WordListToDeleteViewModel
            {
                TotalCount = count,
                Words = words
            };
        }

        public void DeleteWords(IEnumerable<string> words)
        {
            var removeWordListCommand = new RemoveWordListCommand {WordList = words};
            commandDispatcher.Dispatch<RemoveWordListCommand, VoidCommandResponse>(removeWordListCommand);
        }
    }
}
