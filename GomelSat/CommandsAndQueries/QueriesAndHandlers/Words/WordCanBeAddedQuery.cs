using System;
using Common.CommandQueryTools;

namespace CommandsAndQueries.QueriesAndHandlers.Words
{
    public class WordCanBeAddedQuery : IQuery<bool>
    {
        public string Word { get; set; }
    }
}
