using System.Data.Entity;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;
using DataBase.Models.AnalizingTextModels;

namespace CommandsAndQueries.QueriesAndHandlers.AnalizingTexts
{
    public class GetReviewingTextByIdQueryHandler : IQueryHandler<GetReviewingTextByIdQuery, AnalizingTextDataBaseModel>
    {
        private readonly DataBaseContext context;

        public GetReviewingTextByIdQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public AnalizingTextDataBaseModel Handle(GetReviewingTextByIdQuery query)
        {
            var model = context
                .AnalizingTextModels
                .Include(baseModel => baseModel.ImageLink)
                .Include(baseModel => baseModel.SourceLink)
                .FirstOrDefault(baseModel => baseModel.Id == query.Id);

            return model;
        }
    }
}
