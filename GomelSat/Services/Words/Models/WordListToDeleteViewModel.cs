using System.Collections.Generic;

namespace Services.Words.Models
{
    public class WordListToDeleteViewModel
    {
        public IEnumerable<string> Words { get; set; }

        public int TotalCount { get; set; }
    }
}
