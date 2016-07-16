namespace Common.CommandQueryTools
{
    public interface IQueryDispatcher
    {
        TQueryResponse Dispatch<TQuery, TQueryResponse>(TQuery query) where TQuery : IQuery<TQueryResponse>;
    }
}
