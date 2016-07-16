using System.Data.Entity;
using DataBase.Configurations.AnalizingTextModels;
using DataBase.Configurations.NewsModels;
using DataBase.Configurations.SettingsModels;
using DataBase.Configurations.WordModels;
using DataBase.Models.AnalizingTextModels;
using DataBase.Models.NewsModels;
using DataBase.Models.SettingsModels;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GomelSatNewsModelConfigurations());
            modelBuilder.Configurations.Add(new RequestRecordModelConfigurations());
            modelBuilder.Configurations.Add(new AnalizingTextModelConfigurations());
            modelBuilder.Configurations.Add(new WordModelConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}
