using System;
using Common.CommandQueryTools;
using Common.Enums;

namespace CommandsAndQueries.QueriesAndHandlers.RequestRecords
{
    public class GetLastRequestDateQuery : IQuery<DateTimeOffset>
    {
        public SiteName SiteName { get; set; }
    }
}
