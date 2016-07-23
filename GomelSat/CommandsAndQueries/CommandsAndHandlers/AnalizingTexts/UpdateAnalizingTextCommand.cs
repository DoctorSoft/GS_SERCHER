using Common.CommandQueryTools;
using DataBase.Models.AnalizingTextModels;

namespace CommandsAndQueries.CommandsAndHandlers.AnalizingTexts
{
    public class UpdateAnalizingTextCommand : IVoidCommand
    {
        public AnalizingTextDataBaseModel AnalizingText { get; set; }
    }
}
