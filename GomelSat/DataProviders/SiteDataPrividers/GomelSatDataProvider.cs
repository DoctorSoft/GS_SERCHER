using System.Collections.Generic;
using System.Net.Http;
using DataProviders.Constants;

namespace DataProviders.SiteDataPrividers
{
    public class GomelSatDataProvider : ISiteDataProvider
    {
        private readonly HttpClient httpClient;

        public GomelSatDataProvider()
        {
            this.httpClient = new HttpClient();
        }

        public string GetPageData(long page = SiteConstants.StartPage)
        {
            var httpAddress = string.Format(SiteConstants.GomelSatSitePagePattern, page);

            var response = httpClient.GetStringAsync(httpAddress).Result;

            return response;
        }

        public IEnumerable<string> GetPagesData(long startPage = SiteConstants.StartPage, long endPage = SiteConstants.EndPage)
        {
            for (var page = startPage; page <= endPage; page++)
            {
                yield return GetPageData(page);
            }
        }

        public string GetNewsPageContentByUrl(string url)
        {
            var response = httpClient.GetStringAsync(url).Result;

            return response;
        }
    }
}
