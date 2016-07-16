using System;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;
using DataBase.Models.NewsModels;
using DataParsers.Models;
using DataParsers.Models.ModelTools;

namespace CommandsAndQueries.CommandsAndHandlers.GomelSatNews
{
    public class SynchronizeGomelSatNewsCommandHandler : IVoidCommandHandler<SynchronizeGomelSatNewsCommand>
    {
        private readonly DataBaseContext context;

        public SynchronizeGomelSatNewsCommandHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public VoidCommandResponse Handle(SynchronizeGomelSatNewsCommand command)
        {
            var outBaseNews = command.SynchronizingModels.ToList();

            var inBaseNews = context
                .GomelSatNewsModels
                .OrderByDescending(model => model.Id)
                .Take(outBaseNews.Count * 2) //// It is enough to find collisions 
                .Select(model => new GomelSatNewsHeaderModel
                {
                    Link = model.Link,
                    HeaderName = model.HeaderName,
                    HeaderText = model.HeaderText
                })
                .ToList();

            var newNews = outBaseNews.Except(inBaseNews, new GomelSatNewsHeaderModelEqualityComparer());

            context.GomelSatNewsModels.AddRange(newNews.Select(model => new GomelSatNewsDataBaseModel
            {
                Link = model.Link,
                Text = null,
                HeaderName = model.HeaderName,
                HeaderText = model.HeaderText,
                Id = 0,
                CreationDateTimeOffset = DateTimeOffset.Now
            }));

            context.SaveChanges();

            return new VoidCommandResponse();
        }
    }
}
