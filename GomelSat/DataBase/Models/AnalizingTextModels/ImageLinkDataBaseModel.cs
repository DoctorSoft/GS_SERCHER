using System.Collections.Generic;

namespace DataBase.Models.AnalizingTextModels
{
    public class ImageLinkDataBaseModel
    {
        public long Id { get; set; }

        public string Link { get; set; }

        public ICollection<AnalizingTextDataBaseModel> AnalizingTexts { get; set; }
    }
}
