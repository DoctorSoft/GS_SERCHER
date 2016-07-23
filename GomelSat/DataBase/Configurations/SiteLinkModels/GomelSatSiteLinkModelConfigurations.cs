using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataBase.Models.SiteLinkModels;

namespace DataBase.Configurations.SiteLinkModels
{
    public class GomelSatSiteLinkModelConfigurations : EntityTypeConfiguration<GomelSatSiteLinkDataBaseModel>
    {
        public GomelSatSiteLinkModelConfigurations()
        {
            ToTable("GomelSatSiteLinks");

            HasKey(model => model.Id);
            Property(model => model.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(model => model.Link);
            Property(model => model.Priority);
            Property(model => model.Name);
        }
    }
}
