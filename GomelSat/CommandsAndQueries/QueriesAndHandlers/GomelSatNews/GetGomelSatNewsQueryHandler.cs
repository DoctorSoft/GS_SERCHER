using System.Collections.Generic;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;
using DataParsers.Models;

namespace CommandsAndQueries.QueriesAndHandlers.GomelSatNews
{
    public class GetGomelSatNewsQueryHandler : IQueryHandler<GetGomelSatNewsQuery, IEnumerable<GomelSatNewsModel>>
    {
        private readonly DataBaseContext context;

        public GetGomelSatNewsQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<GomelSatNewsModel> Handle(GetGomelSatNewsQuery query)
        {
            var news = context
                .GomelSatNewsModels
                .OrderByDescending(model => model.Id)
                .Take(query.Count)
                .Select(model => new GomelSatNewsModel
                {
                    Link = model.Link,
                    Text = model.Text,
                    HeaderName = model.HeaderName,
                    HeaderText = model.HeaderText
                })
                .ToList();

            return news;
        }
    }
}
