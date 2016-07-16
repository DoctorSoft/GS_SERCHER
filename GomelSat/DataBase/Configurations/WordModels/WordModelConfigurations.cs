using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataBase.Models.WordModels;

namespace DataBase.Configurations.WordModels
{
    public class WordModelConfigurations : EntityTypeConfiguration<WordDataBaseModel>
    {
        public WordModelConfigurations()
        {
            ToTable("Word");

            HasKey(model => model.Id);
            Property(model => model.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(model => model.Word);
        }
    }
}
