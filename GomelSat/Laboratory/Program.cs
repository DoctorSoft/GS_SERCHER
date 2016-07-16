using DataParsers;
using DataParsers.NewsParsers;
using DataProviders;
using DataProviders.SiteDataPrividers;

namespace Laboratory
{
    public class Program
    {
        static void Main(string[] args)
        {
            var provider = new GomelSatDataProvider();
            var parser = new GomelSatNewsHeadersParser();

            var data = provider.GetPageData();
            var news = parser.GetPageNewsHeaders(data);
        }
    }
}
