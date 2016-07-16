using Common.CommandQueryTools;
using Common.Enums;

namespace CommandsAndQueries.CommandsAndHandlers.RequestRecords
{
    public class UpdateLastRequestDateCommand : IVoidCommand
    {
        public SiteName SiteName { get; set; }
    }
}
