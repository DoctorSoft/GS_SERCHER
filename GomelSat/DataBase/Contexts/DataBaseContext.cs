using System.Data.Entity;
using DataBase.Configurations.AnalizingTextModels;
using DataBase.Configurations.NewsModels;
using DataBase.Configurations.SettingsModels;
using DataBase.Configurations.SiteLinkModels;
using DataBase.Configurations.WordModels;
using DataBase.Models.AnalizingTextModels;
using DataBase.Models.NewsModels;
using DataBase.Models.SettingsModels;
using DataBase.Models.SiteLinkModels;
using DataBase.Models.WordModels;

namespace DataBase.Contexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            :base("CommonDataBase")
        {
            
        }

        public DbSet<GomelSatNewsDataBaseModel> GomelSatNewsModels { get; set; }

        public DbSet<RequestRecordDataBaseModel> RequestRecordModels { get; set; }

        public DbSet<AnalizingTextDataBaseModel> AnalizingTextModels { get; set; }

        public DbSet<WordDataBaseModel> WordModels { get; set; }

        public DbSet<GomelSatSiteLinkDataBaseModel> GomelSatSiteLinkModels { get; set; }

        public DbSet<SourceLinkDataBaseModel> SourceLinks { get; set; }

        public DbSet<ImageLinkDataBaseModel> ImageLinks { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GomelSatNewsModelConfigurations());
            modelBuilder.Configurations.Add(new RequestRecordModelConfigurations());
            modelBuilder.Configurations.Add(new AnalizingTextModelConfigurations());
            modelBuilder.Configurations.Add(new WordModelConfigurations());
            modelBuilder.Configurations.Add(new GomelSatSiteLinkModelConfigurations());
            modelBuilder.Configurations.Add(new SourceLinkModelConfigurations());
            modelBuilder.Configurations.Add(new ImageLinkModelConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}
