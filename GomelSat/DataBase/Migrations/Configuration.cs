using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DataBase.Models.SettingsModels;
using DataBase.Models.SiteLinkModels;
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
            // records
            if (!context.RequestRecordModels.Any(model => model.SiteName == SiteName.GomelSat))
            {
                context.RequestRecordModels.Add(new RequestRecordDataBaseModel
                {
                    Id = 0,
                    SiteName = SiteName.GomelSat,
                    LastRequest = DateTimeOffset.MinValue
                });
            }

            // words
            try
            {
                if (!context.WordModels.Any())
                {
                    var words = new WordFileManager().GetWords();
                    foreach (var word in words)
                    {
                        if (!context.WordModels.Any(model => model.Word.ToUpper() == word.ToUpper()))
                        {
                            context.WordModels.Add(new WordDataBaseModel {Word = word});
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
            
            // gomel sat site links
            if (!context.GomelSatSiteLinkModels.Any())
            {
                var gomelSatSiteLinks = new List<GomelSatSiteLinkDataBaseModel>
                {
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://telesat-news.net/",
                        Name = "telesat-news",
                        Priority = 1
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://tvnews.by/",
                        Name = "tvnews.by",
                        Priority = 2
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://mediasat.info/",
                        Name = "mediasat.info",
                        Priority = 3
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://www.cableman.ru/news",
                        Name = "cableman.ru",
                        Priority = 4
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://www.tdaily.ru/news/top-novosti",
                        Name = "tdaily.ru",
                        Priority = 6
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://www.ferra.ru/",
                        Name = "ferra.ru",
                        Priority = 5
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://www.sat-expert.com/",
                        Name = "sat-expert.com",
                        Priority = 7
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://telesputnik.ru/",
                        Name = "telesputnik.ru",
                        Priority = 8
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://www.tv-digest.ru/",
                        Name = "tv-digest.ru",
                        Priority = 9
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://sputnik.by/technology/",
                        Name = "sputnik.by",
                        Priority = 11
                    },
                    new GomelSatSiteLinkDataBaseModel
                    {
                        Link = "http://news.sputnik.ru/progress",
                        Name = "news.sputnik.ru",
                        Priority = 10
                    }
                };
                context.GomelSatSiteLinkModels.AddRange(gomelSatSiteLinks);
            }

        }
    }
}
