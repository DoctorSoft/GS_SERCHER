using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataBase.Models.AnalizingTextModels;

namespace DataBase.Configurations.AnalizingTextModels
{
    public class AnalizingTextModelConfigurations : EntityTypeConfiguration<AnalizingTextDataBaseModel>
    {
        public AnalizingTextModelConfigurations()
        {
            ToTable("AnalizingText");

            HasKey(model => model.Id);
            Property(model => model.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(model => model.HeaderText);
            Property(model => model.ContentText);
        }
    }
}
