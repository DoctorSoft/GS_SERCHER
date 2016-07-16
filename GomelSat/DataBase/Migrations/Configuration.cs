using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DataBase.Models.SettingsModels;
using DataBase.Models.WordModels;
using FilesManagers.WordFileManagers;

namespace DataBase.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataBase.Contexts.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataBase.Contexts.DataBaseContext context)
        {
            if (!context.RequestRecordModels.Any(model => model.SiteName == SiteName.GomelSat))
            {
                context.RequestRecordModels.Add(new RequestRecordDataBaseModel
                {
                    Id = 0,
                    SiteName = SiteName.GomelSat,
                    LastRequest = DateTimeOffset.MinValue
                });
            }
            
            var words = new WordFileManager().GetWords();
            foreach (var word in words)
            {
                if (!context.WordModels.Any(model => model.Word.ToUpper() == word.ToUpper()))
                {
                    context.WordModels.Add(new WordDataBaseModel { Word = word });
                }
            }
        }
    }
}
