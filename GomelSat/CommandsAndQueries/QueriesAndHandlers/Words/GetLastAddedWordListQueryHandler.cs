using System.Collections.Generic;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;

namespace CommandsAndQueries.QueriesAndHandlers.Words
{
    public class GetLastAddedWordListQueryHandler : IQueryHandler<GetLastAddedWordListQuery, IEnumerable<string>>
    {
        private readonly DataBaseContext context;

        public GetLastAddedWordListQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> Handle(GetLastAddedWordListQuery query)
        {
            var words = context
                .WordModels
                .OrderByDescending(model => model.Id)
                .Select(model => model.Word)
                .Take(query.Count)
                .ToList();

            return words;
        }
    }
}
