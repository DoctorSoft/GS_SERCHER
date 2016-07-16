using Common.CommandQueryTools;

namespace CommandsAndQueries.CommandsAndHandlers.AnalizingTexts
{
    public class AddAnalizingTextCommand : ICommand<long>
    {
        public string HeaderText { get; set; }

        public string ContentText { get; set; }
    }
}
