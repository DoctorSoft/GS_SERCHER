using System;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;

namespace CommandsAndQueries.QueriesAndHandlers.RequestRecords
{
    public class GetLastRequestDateQueryHandler : IQueryHandler<GetLastRequestDateQuery, DateTimeOffset>
    {
        private readonly DataBaseContext context;

        public GetLastRequestDateQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public DateTimeOffset Handle(GetLastRequestDateQuery query)
        {
            var lastRequestData = context.RequestRecordModels.FirstOrDefault(model => model.SiteName == query.SiteName);

            return lastRequestData.LastRequest;
        }
    }
}
