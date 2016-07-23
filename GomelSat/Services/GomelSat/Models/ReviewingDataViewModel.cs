namespace Services.GomelSat.Models
{
    public class ReviewingDataViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string ShortText { get; set; }

        public string Text { get; set; }

        public bool ImageExists { get; set; }

        public bool LinkExists { get; set; }

        public bool TitleExists { get; set; }
    }
}
