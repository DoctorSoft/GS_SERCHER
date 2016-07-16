using System.Security.Cryptography.X509Certificates;

namespace Common.CommandQueryTools
{
    public interface IQueryHandler<TQuery, TQueryResponse> where TQuery: IQuery<TQueryResponse>
    {
        TQueryResponse Handle(TQuery query);
    }
}
