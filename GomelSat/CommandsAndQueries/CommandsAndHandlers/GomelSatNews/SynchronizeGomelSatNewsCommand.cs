using System.Collections.Generic;
using Common.CommandQueryTools;
using DataParsers.Models;

namespace CommandsAndQueries.CommandsAndHandlers.GomelSatNews
{
    public class SynchronizeGomelSatNewsCommand : IVoidCommand
    {
        public IEnumerable<GomelSatNewsHeaderModel> SynchronizingModels { get; set; }
    }
}
