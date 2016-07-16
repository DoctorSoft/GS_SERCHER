using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;

namespace CommandsAndQueries.QueriesAndHandlers.Words
{
    public class WordCanBeAddedQueryHandler : IQueryHandler<WordCanBeAddedQuery, bool>
    {
        private readonly DataBaseContext context;

        public WordCanBeAddedQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public bool Handle(WordCanBeAddedQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Word))
            {
                return false;
            }

            var wordExists = context
                .WordModels
                .Any(model => model.Word.ToUpper() == query.Word.ToUpper());

            return !wordExists;
        }
    }
}
