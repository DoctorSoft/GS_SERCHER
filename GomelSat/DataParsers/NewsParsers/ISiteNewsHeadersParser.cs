using System.Collections.Generic;

namespace DataParsers.NewsParsers
{
    public interface ISiteNewsHeadersParser<out TSiteNewsHeaderModel>
    {
        IEnumerable<TSiteNewsHeaderModel> GetPageNewsHeaders(string pageText);
    }
}
