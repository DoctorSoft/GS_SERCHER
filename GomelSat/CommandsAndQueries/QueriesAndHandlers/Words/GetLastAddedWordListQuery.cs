using System.Collections.Generic;
using Common.CommandQueryTools;

namespace CommandsAndQueries.QueriesAndHandlers.Words
{
    public class GetLastAddedWordListQuery : IQuery<IEnumerable<string>>
    {
        public int Count { get; set; }
    }
}
