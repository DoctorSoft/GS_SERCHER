using System.Collections.Generic;
using Common.CommandQueryTools;
using DataParsers.Models;

namespace CommandsAndQueries.CommandsAndHandlers.GomelSatNews
{
    public class AddGomelSatNewsContentsCommand : IVoidCommand
    {
        public IEnumerable<GomelSatNewsContentModel> GomelSatNewsContentModels { get; set; } 
    }
}
