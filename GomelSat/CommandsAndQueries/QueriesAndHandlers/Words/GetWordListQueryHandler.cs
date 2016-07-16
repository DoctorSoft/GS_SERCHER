using System.Collections.Generic;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;

namespace CommandsAndQueries.QueriesAndHandlers.Words
{
    public class GetWordListQueryHandler : IQueryHandler<GetWordListQuery, IEnumerable<string>>
    {
        private readonly DataBaseContext context;

        public GetWordListQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> Handle(GetWordListQuery query)
        {
            var words = context
                .WordModels
                .OrderBy(model => model.Word)
                .Select(model => model.Word)
                .ToList();

            return words;
        }
    }
}
