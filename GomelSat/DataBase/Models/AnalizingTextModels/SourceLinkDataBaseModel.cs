using System.Collections.Generic;

namespace DataBase.Models.AnalizingTextModels
{
    public class SourceLinkDataBaseModel
    {
        public long Id { get; set; }

        public string Link { get; set; }

        public ICollection<AnalizingTextDataBaseModel> AnalizingTexts { get; set; }
    }
}
