using System.Collections.Generic;
using System.Linq;
using Common.CommandQueryTools;
using DataBase.Contexts;
using DataBase.Models.SiteLinkModels;

namespace CommandsAndQueries.QueriesAndHandlers.GomelSatSiteLinks
{
    public class GetGomelSatSiteLinksQueryHandler : IQueryHandler<GetGomelSatSiteLinksQuery, IEnumerable<GomelSatSiteLinkDataBaseModel>>
    {
        private readonly DataBaseContext context;

        public GetGomelSatSiteLinksQueryHandler(DataBaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<GomelSatSiteLinkDataBaseModel> Handle(GetGomelSatSiteLinksQuery query)
        {
            var list = context
                .GomelSatSiteLinkModels
                .OrderBy(model => model.Priority)
                .ToList();

            return list;
        }
    }
}
