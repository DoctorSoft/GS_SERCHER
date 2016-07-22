using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Linq;
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
            Property(model => model.ImageLinkId);
            
            HasOptional(it => it.ImageLink)
                .WithMany(model => model.AnalizingTexts)
                .HasForeignKey(model => model.ImageLinkId);

            HasOptional(it => it.SourceLink)
                .WithMany(model => model.AnalizingTexts)
                .HasForeignKey(model => model.SourceLinkId);
        }
    }
}
