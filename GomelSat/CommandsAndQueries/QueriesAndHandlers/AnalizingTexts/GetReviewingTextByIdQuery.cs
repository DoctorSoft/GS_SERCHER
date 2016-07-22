using Common.CommandQueryTools;
using DataBase.Models.AnalizingTextModels;
using TextAnalizators.Models;

namespace CommandsAndQueries.QueriesAndHandlers.AnalizingTexts
{
    public class GetReviewingTextByIdQuery : IQuery<AnalizingTextDataBaseModel>
    {
        public long Id { get; set; }
    }
}
