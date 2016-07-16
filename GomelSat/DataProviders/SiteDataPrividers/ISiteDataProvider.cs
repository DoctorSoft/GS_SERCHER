using System.Collections.Generic;
using DataProviders.Constants;

namespace DataProviders.SiteDataPrividers
{
    public interface ISiteDataProvider
    {
        string GetPageData(long page = SiteConstants.StartPage);

        IEnumerable<string> GetPagesData(long startPage = SiteConstants.StartPage, long endPage = SiteConstants.EndPage);

        string GetNewsPageContentByUrl(string url);
    }
}
