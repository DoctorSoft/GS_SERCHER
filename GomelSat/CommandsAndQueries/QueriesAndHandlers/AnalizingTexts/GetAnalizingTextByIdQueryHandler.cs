using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;
using TextAnalizators.Models;

namespace CommandsAndQueries.QueriesAndHandlers.AnalizingTexts
{
    public class GetAnalizingTextByIdQueryHandler : IQueryHandler<GetAnalizingTextByIdQuery, AnalizingTextModel>
    {
        private readonly DataBaseContext context;

        public GetAnalizingTextByIdQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public AnalizingTextModel Handle(GetAnalizingTextByIdQuery query)
        {
            var model = context
                .AnalizingTextModels
                .Where(baseModel => baseModel.Id == query.Id)
                .Select(baseModel => new AnalizingTextModel
                {
                    NewsHeader = baseModel.HeaderText,
                    NewsText = baseModel.ContentText
                })
                .FirstOrDefault();

            return model;
        }
    }
}
