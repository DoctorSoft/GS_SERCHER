using System.Data.Entity.Migrations;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;

namespace CommandsAndQueries.CommandsAndHandlers.GomelSatNews
{
    public class AddGomelSatNewsContentsCommandHandler : IVoidCommandHandler<AddGomelSatNewsContentsCommand>
    {
        private readonly DataBaseContext context;

        public AddGomelSatNewsContentsCommandHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public VoidCommandResponse Handle(AddGomelSatNewsContentsCommand command)
        {
            var contents = command.GomelSatNewsContentModels.ToList();

            foreach (var gomelSatNewsContentModel in contents)
            {
                var newsToUpdate =
                    context.GomelSatNewsModels.FirstOrDefault(
                        model => model.Link.ToUpper() == gomelSatNewsContentModel.Link.ToUpper());

                newsToUpdate.Text = gomelSatNewsContentModel.Text;
                context.GomelSatNewsModels.AddOrUpdate(newsToUpdate);
            }

            context.SaveChanges();

            return new VoidCommandResponse();
        }
    }
}
