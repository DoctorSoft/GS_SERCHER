using System;
using System.Collections.Generic;
using System.Net.Http;
using DataProviders.Constants;

namespace DataProviders.SiteDataPrividers
{
    public class GomelSatDataProvider : ISiteDataProvider
    {
        private readonly HttpClient httpClient = new HttpClient();

        public string GetPageData(long page = SiteConstants.StartPage)
        {
            var httpAddress = string.Format(SiteConstants.GomelSatSitePagePattern, page);

            string response;
            try
            {
                response = httpClient.GetStringAsync(httpAddress).Result;
            }
            catch (Exception)
            {
                response = null;
            }

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
            string response;

            try
            {
                response = httpClient.GetStringAsync(url).Result;
            }
            catch
            {
                response = null;
            }

            return response;
        }
    }
}
