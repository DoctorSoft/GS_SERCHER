using System.Collections.Generic;
using Common.CommandQueryTools;
using DataParsers.Models;

namespace CommandsAndQueries.QueriesAndHandlers.GomelSatNews
{
    public class GetGomelSatNewsQuery : IQuery<IEnumerable<GomelSatNewsModel>>
    {
        public int Count { get; set; }
    }
}
