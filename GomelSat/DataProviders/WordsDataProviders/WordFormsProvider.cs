using System.Net.Http;
using DataProviders.Constants;

namespace DataProviders.WordsDataProviders
{
    public class WordFormsProvider : IWordFormsProvider
    {
        public string GetWordFormsData(string word)
        {
            using (var httpClient = new HttpClient())
            {
                var httpAddress = string.Format(SiteConstants.WordFormSiteWordPattern, word);

                var response = httpClient.GetStringAsync(httpAddress).Result;

                return response;
            }
        }
    }
}
