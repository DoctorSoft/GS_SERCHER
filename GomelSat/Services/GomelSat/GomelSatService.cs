using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndQueries.CommandsAndHandlers.AnalizingTexts;
using CommandsAndQueries.CommandsAndHandlers.GomelSatNews;
using CommandsAndQueries.CommandsAndHandlers.RequestRecords;
using CommandsAndQueries.QueriesAndHandlers.AnalizingTexts;
using CommandsAndQueries.QueriesAndHandlers.GomelSatNews;
using CommandsAndQueries.QueriesAndHandlers.RequestRecords;
using Common.CommandQueryTools;
using Common.Constants;
using Common.Enums;
using DataParsers;
using DataParsers.Models;
using DataParsers.NewsParsers;
using DataProviders;
using DataProviders.SiteDataPrividers;
using Services.GomelSat.Models;
using Services.Words;
using TextAnalizators;
using TextAnalizators.Models;

namespace Services.GomelSat
{
    public class GomelSatService : IGomelSatService
    {
        private readonly ISiteNewsHeadersParser<GomelSatNewsHeaderModel> gomelSatNewsHeadersParser;

        private readonly ISiteNewsContentParser<GomelSatNewsContentModel> gomelSatNewsContentParser;

        private readonly IWordService wordService;

        private readonly ISiteDataProvider gomelSatDataProvider;

        private readonly ICommandDispatcher commandDispatcher;

        private readonly IQueryDispatcher queryDispatcher;

        private readonly ITextAnalizator<GomelSatNewsModel> gomelSaTextAnalizator;

        public GomelSatService(ISiteNewsHeadersParser<GomelSatNewsHeaderModel> gomelSatNewsHeadersParser, ISiteDataProvider gomelSatDataProvider, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ISiteNewsContentParser<GomelSatNewsContentModel> gomelSatNewsContentParser, ITextAnalizator<GomelSatNewsModel> gomelSaTextAnalizator, IWordService wordService)
        {
            this.gomelSatNewsHeadersParser = gomelSatNewsHeadersParser;
            this.gomelSatDataProvider = gomelSatDataProvider;
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.gomelSatNewsContentParser = gomelSatNewsContentParser;
            this.gomelSaTextAnalizator = gomelSaTextAnalizator;
            this.wordService = wordService;
        }

        public IEnumerable<GomelSatNewsModel> GetNews()
        {
            var getLastRequestDateQuery = new GetLastRequestDateQuery {SiteName = SiteName.GomelSat};
            var lastRequestTime = queryDispatcher.Dispatch<GetLastRequestDateQuery, DateTimeOffset>(getLastRequestDateQuery);

            if (DateTimeOffset.Now - lastRequestTime > GomelSatConstants.RequestTimeDiapason)
            {
                SynchonizeNewsWithSite();
            }

            var getGomelSatNewsQuery = new GetGomelSatNewsQuery {Count = GomelSatConstants.NewsCount};
            var news = queryDispatcher.Dispatch<GetGomelSatNewsQuery, IEnumerable<GomelSatNewsModel>>(getGomelSatNewsQuery);

            return news;
        }

        public long SaveAnalizingText(string newsHeader, string newsText)
        {
            var addAnalizingTextCommand = new AddAnalizingTextCommand { HeaderText = newsHeader, ContentText = newsText };
            var textId = commandDispatcher.Dispatch<AddAnalizingTextCommand, long>(addAnalizingTextCommand);

            return textId;
        }

        public AnalizedDataViewModel GetAnalizedData(long id)
        {
            var getAnalizingTextByIdQuery = new GetAnalizingTextByIdQuery {Id = id};
            var analizingText = queryDispatcher.Dispatch<GetAnalizingTextByIdQuery, AnalizingTextModel>(getAnalizingTextByIdQuery);

            var news = GetNews().ToList();

            var banList = wordService.GetWordList().ToList();

            var wordList = gomelSaTextAnalizator.GetNewsWordList(analizingText, banList);
            var analizedResults = news
                .Select(model => gomelSaTextAnalizator.Analize(model, wordList, banList))
                .OrderByDescending(model => model.FoundWordsCount)
                .ToList();

            return new AnalizedDataViewModel
            {
                AnalizedTextModels = analizedResults,
                ContentText = analizingText.NewsText,
                HeaderText = analizingText.NewsHeader
            };
        }

        public void RefreshNews()
        {
            var removeLastGomelSatNewsCommand = new RemoveLastGomelSatNewsCommand { Count = GomelSatConstants.NewsCountToRefresh };
            commandDispatcher.Dispatch<RemoveLastGomelSatNewsCommand, VoidCommandResponse>(removeLastGomelSatNewsCommand);

            SynchonizeNewsWithSite();
        }

        private void SynchonizeNewsWithSite()
        {
            var pagesData = gomelSatDataProvider.GetPagesData();
            var headers = pagesData
                .SelectMany(s => gomelSatNewsHeadersParser.GetPageNewsHeaders(s))
                .Where(model => !string.IsNullOrWhiteSpace(model.HeaderText) || !string.IsNullOrWhiteSpace(model.Link))
                .Reverse()
                .ToList();

            var synchronizeGomelSatNewsCommand = new SynchronizeGomelSatNewsCommand { SynchronizingModels = headers };
            commandDispatcher.Dispatch<SynchronizeGomelSatNewsCommand, VoidCommandResponse>(synchronizeGomelSatNewsCommand);

            var updateLastRequestDateCommand = new UpdateLastRequestDateCommand { SiteName = SiteName.GomelSat };
            commandDispatcher.Dispatch<UpdateLastRequestDateCommand, VoidCommandResponse>(updateLastRequestDateCommand);

            var getGomelSatNewsWithoutContentQuery = new GetGomelSatNewsWithoutContentQuery();
            var newsWithoutContentModels = queryDispatcher.Dispatch<GetGomelSatNewsWithoutContentQuery, IEnumerable<GomelSatNewsModel>>(getGomelSatNewsWithoutContentQuery);

            var newsContentModels = newsWithoutContentModels.Select(model => gomelSatNewsContentParser
                .GetContent(new GomelSatNewsContentModel
                {
                    Text = gomelSatDataProvider.GetNewsPageContentByUrl(model.Link),
                    Link = model.Link
                }
            )).ToList();

            var addGomelSatNewsContentsCommand = new AddGomelSatNewsContentsCommand
            {
                GomelSatNewsContentModels = newsContentModels
            };
            commandDispatcher.Dispatch<AddGomelSatNewsContentsCommand, VoidCommandResponse>(addGomelSatNewsContentsCommand);
        }
    }
}
