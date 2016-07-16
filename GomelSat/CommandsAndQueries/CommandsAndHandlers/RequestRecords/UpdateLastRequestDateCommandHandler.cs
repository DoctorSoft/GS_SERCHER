using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;

namespace CommandsAndQueries.CommandsAndHandlers.RequestRecords
{
    public class UpdateLastRequestDateCommandHandler : IVoidCommandHandler<UpdateLastRequestDateCommand>
    {
        private readonly DataBaseContext context;

        public UpdateLastRequestDateCommandHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public VoidCommandResponse Handle(UpdateLastRequestDateCommand command)
        {
            var lastRequestData = context.RequestRecordModels.FirstOrDefault(model => model.SiteName == command.SiteName);

            lastRequestData.LastRequest = DateTimeOffset.Now;

            context.RequestRecordModels.AddOrUpdate(lastRequestData);
            context.SaveChanges();

            return new VoidCommandResponse();
        }
    }
}
