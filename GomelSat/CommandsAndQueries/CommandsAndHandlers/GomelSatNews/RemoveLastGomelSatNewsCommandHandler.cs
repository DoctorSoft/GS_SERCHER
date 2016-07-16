using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;

namespace CommandsAndQueries.CommandsAndHandlers.GomelSatNews
{
    public class RemoveLastGomelSatNewsCommandHandler : IVoidCommandHandler<RemoveLastGomelSatNewsCommand>
    {
        private readonly DataBaseContext context;

        public RemoveLastGomelSatNewsCommandHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public VoidCommandResponse Handle(RemoveLastGomelSatNewsCommand command)
        {
            var newsToDelete = context
                .GomelSatNewsModels
                .OrderByDescending(model => model.Id)
                .Take(command.Count)
                .ToList();

            context.GomelSatNewsModels.RemoveRange(newsToDelete);

            context.SaveChanges();

            return new VoidCommandResponse();
        }
    }
}
