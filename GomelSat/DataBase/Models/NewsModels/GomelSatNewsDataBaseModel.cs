using System;

namespace DataBase.Models.NewsModels
{
    public class GomelSatNewsDataBaseModel
    {
        public long Id { get; set; }

        public string Link { get; set; } //// Unique key

        public string HeaderName { get; set; }

        public string HeaderText { get; set; }

        public string Text { get; set; }

        public DateTimeOffset CreationDateTimeOffset { get; set; }
    }
}
