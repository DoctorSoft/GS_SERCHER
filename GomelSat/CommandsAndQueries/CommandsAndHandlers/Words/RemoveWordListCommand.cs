using System.Collections.Generic;
using Common.CommandQueryTools;

namespace CommandsAndQueries.CommandsAndHandlers.Words
{
    public class RemoveWordListCommand : IVoidCommand
    {
        public IEnumerable<string> WordList { get; set; } 
    }
}
