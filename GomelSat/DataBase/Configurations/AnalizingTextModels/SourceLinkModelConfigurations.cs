using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataBase.Models.AnalizingTextModels;

namespace DataBase.Configurations.AnalizingTextModels
{
    public class SourceLinkModelConfigurations: EntityTypeConfiguration<SourceLinkDataBaseModel>
    {
        public SourceLinkModelConfigurations()
        {
            ToTable("SourceLinks");

            HasKey(model => model.Id);
            Property(model => model.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(model => model.Link);

            HasMany(it => it.AnalizingTexts)
                .WithOptional(model => model.SourceLink)
                .HasForeignKey(model => model.SourceLinkId);
        }
    }
}
