using System;
using Common.Enums;

namespace DataBase.Models.SettingsModels
{
    public class RequestRecordDataBaseModel
    {
        public long Id { get; set; }

        public SiteName SiteName { get; set; } //// Unique key

        public DateTimeOffset LastRequest { get; set; }
    }
}
