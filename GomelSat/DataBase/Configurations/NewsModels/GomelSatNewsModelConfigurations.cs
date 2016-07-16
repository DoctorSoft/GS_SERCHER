using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataBase.Models.NewsModels;

namespace DataBase.Configurations.NewsModels
{
    public class GomelSatNewsModelConfigurations : EntityTypeConfiguration<GomelSatNewsDataBaseModel>
    {
        public GomelSatNewsModelConfigurations()
        {
            ToTable("GomelSatNews");

            HasKey(model => model.Id);
            Property(model => model.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(model => model.HeaderName);
            Property(model => model.HeaderText);
            Property(model => model.HeaderName);
            Property(model => model.Text);
            Property(model => model.Link);
            Property(model => model.CreationDateTimeOffset);
        }
    }
}
