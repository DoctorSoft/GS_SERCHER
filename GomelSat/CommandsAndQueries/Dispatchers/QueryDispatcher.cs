using System;
using System.Linq;
using Common.CommandQueryTools;

namespace CommandsAndQueries.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly object[] queryHandlers;

        public QueryDispatcher(object[] queryHandlers)
        {
            this.queryHandlers = queryHandlers;
        }

        public TQueryResponse Dispatch<TQuery, TQueryResponse>(TQuery query) where TQuery : IQuery<TQueryResponse>
        {
            foreach (var handler in queryHandlers.OfType<IQueryHandler<TQuery, TQueryResponse>>())
            {
                var response = handler.Handle(query);
                return response;
            }

            throw new Exception(String.Format("Please connect query {0} to dispatcher", query));
        }
    }
}
