using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataBase.Models.AnalizingTextModels;

namespace DataBase.Configurations.AnalizingTextModels
{
    public class ImageLinkModelConfigurations : EntityTypeConfiguration<ImageLinkDataBaseModel>
    {
        public ImageLinkModelConfigurations()
        {
            ToTable("ImageLinks");

            HasKey(model => model.Id);
            Property(model => model.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(model => model.Link);

            HasMany(it => it.AnalizingTexts)
                .WithOptional(model => model.ImageLink)
                .HasForeignKey(model => model.ImageLinkId);
        }
    }
}
