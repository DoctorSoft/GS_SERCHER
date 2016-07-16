using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DataParsers.WordParsers
{
    public class WordFormsDataParser : IWordFormsDataParser
    {
        private readonly JavaScriptSerializer jsonSerializer;

        public WordFormsDataParser()
        {
            this.jsonSerializer = new JavaScriptSerializer();
        }

        public IEnumerable<string> GetWordForms(string json)
        {
            var limitField = "limit";
            var errorField = "error";

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            
            dictionary.Remove(limitField);
            dictionary.Remove(errorField);
            
            return dictionary.Values.Select(s => s.ToLower()).ToList();
        }
    }
}
