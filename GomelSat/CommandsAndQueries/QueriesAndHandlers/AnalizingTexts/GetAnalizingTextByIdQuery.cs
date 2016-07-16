using Common.CommandQueryTools;
using TextAnalizators.Models;

namespace CommandsAndQueries.QueriesAndHandlers.AnalizingTexts
{
    public class GetAnalizingTextByIdQuery : IQuery<AnalizingTextModel>
    {
        public long Id { get; set; }
    }
}
