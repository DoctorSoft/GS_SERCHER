namespace DataBase.Models.AnalizingTextModels
{
    public class AnalizingTextDataBaseModel
    {
        public long Id { get; set; }

        public string HeaderText { get; set; }

        public string ContentText { get; set; }

        public long? ImageLinkId { get; set; }

        public long? SourceLinkId { get; set; }

        public ImageLinkDataBaseModel ImageLink { get; set; }

        public SourceLinkDataBaseModel SourceLink { get; set; }
    }
}
