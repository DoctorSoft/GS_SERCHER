using Common.CommandQueryTools;

namespace CommandsAndQueries.CommandsAndHandlers.Words
{
    public class AddWordCommand : IVoidCommand
    {
        public string Word { get; set; }
    }
}
