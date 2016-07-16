using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Enums;
using DataBase.Models.SettingsModels;

namespace DataBase.Configurations.SettingsModels
{
    public class RequestRecordModelConfigurations : EntityTypeConfiguration<RequestRecordDataBaseModel>
    {
        public RequestRecordModelConfigurations()
        {
            ToTable("RequestRecords");

            HasKey(model => model.Id);
            Property(model => model.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(model => model.LastRequest);
            Property(model => model.SiteName);
            
        }
    }
}
