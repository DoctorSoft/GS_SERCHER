using System.Collections.Generic;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;
using DataParsers.Models;

namespace CommandsAndQueries.QueriesAndHandlers.GomelSatNews
{
    public class GetGomelSatNewsWithoutContentQueryHandler : IQueryHandler<GetGomelSatNewsWithoutContentQuery, IEnumerable<GomelSatNewsModel>>
    {
        private readonly DataBaseContext context;

        public GetGomelSatNewsWithoutContentQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<GomelSatNewsModel> Handle(GetGomelSatNewsWithoutContentQuery query)
        {
            var news = context
                .GomelSatNewsModels
                .Where(model => model.Text == null)
                .Select(model => new GomelSatNewsModel
                {
                    Text = model.Text,
                    Link = model.Link,
                    HeaderName = model.HeaderName,
                    HeaderText = model.HeaderText
                })
                .ToList();

            return news;
        }
    }
}
