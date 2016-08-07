using System.Net.Http;
using DataProviders.Constants;

namespace DataProviders.WordsDataProviders
{
    public class WordFormsProvider : IWordFormsProvider
    {
        private readonly HttpClient httpClient = new HttpClient();

        public string GetWordFormsData(string word)
        {
            var httpAddress = string.Format(SiteConstants.WordFormSiteWordPattern, word);

            var response = httpClient.GetStringAsync(httpAddress).Result;

            return response;
        }
    }
}
