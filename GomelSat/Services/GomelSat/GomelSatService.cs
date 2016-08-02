﻿using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndQueries.CommandsAndHandlers.AnalizingTexts;
using CommandsAndQueries.CommandsAndHandlers.GomelSatNews;
using CommandsAndQueries.CommandsAndHandlers.RequestRecords;
using CommandsAndQueries.QueriesAndHandlers.AnalizingTexts;
using CommandsAndQueries.QueriesAndHandlers.GomelSatNews;
using CommandsAndQueries.QueriesAndHandlers.GomelSatSiteLinks;
using CommandsAndQueries.QueriesAndHandlers.RequestRecords;
using Common.CommandQueryTools;
using Common.Constants;
using Common.Enums;
using DataBase.Models.AnalizingTextModels;
using DataBase.Models.SiteLinkModels;
using DataParsers;
using DataParsers.Models;
using DataParsers.NewsParsers;
using DataProviders;
using DataProviders.SiteDataPrividers;
using GomelSatEngine;
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

        private readonly IReviewingTextAnalizator reviewingTextAnalizator;

        private readonly IEnterGomelSatNewsEngine enterGomelSatNewsEngine;

        public GomelSatService(ISiteNewsHeadersParser<GomelSatNewsHeaderModel> gomelSatNewsHeadersParser, ISiteDataProvider gomelSatDataProvider, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ISiteNewsContentParser<GomelSatNewsContentModel> gomelSatNewsContentParser, ITextAnalizator<GomelSatNewsModel> gomelSaTextAnalizator, IWordService wordService, IReviewingTextAnalizator reviewingTextAnalizator, IEnterGomelSatNewsEngine enterGomelSatNewsEngine)
        {
            this.gomelSatNewsHeadersParser = gomelSatNewsHeadersParser;
            this.gomelSatDataProvider = gomelSatDataProvider;
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.gomelSatNewsContentParser = gomelSatNewsContentParser;
            this.gomelSaTextAnalizator = gomelSaTextAnalizator;
            this.wordService = wordService;
            this.reviewingTextAnalizator = reviewingTextAnalizator;
            this.enterGomelSatNewsEngine = enterGomelSatNewsEngine;
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
                .Where(model => model != null)
                .OrderByDescending(model => model.FoundWordsCount)
                .ToList();

            return new AnalizedDataViewModel
            {
                AnalizedTextModels = analizedResults,
                ContentText = analizingText.NewsText,
                HeaderText = analizingText.NewsHeader,
                Id = id
            };
        }

        public void RefreshNews()
        {
            var removeLastGomelSatNewsCommand = new RemoveLastGomelSatNewsCommand { Count = GomelSatConstants.NewsCountToRefresh };
            commandDispatcher.Dispatch<RemoveLastGomelSatNewsCommand, VoidCommandResponse>(removeLastGomelSatNewsCommand);

            SynchonizeNewsWithSite();
        }

        public IEnumerable<SiteLinkViewModel> GetGomelSatSiteLinks()
        {
            var getGomelSatSiteLinksQuery = new GetGomelSatSiteLinksQuery();
            var links = queryDispatcher.Dispatch<GetGomelSatSiteLinksQuery, IEnumerable<GomelSatSiteLinkDataBaseModel>>(getGomelSatSiteLinksQuery);

            return links
                .Select(model => new SiteLinkViewModel
                {
                    Link = model.Link,
                    Name = model.Name
                })
                .ToList();
        }

        public ReviewingDataViewModel GetReviewingData(long id)
        {
            var getReviewingTextByIdQuery = new GetReviewingTextByIdQuery { Id = id };
            var textModel = queryDispatcher.Dispatch<GetReviewingTextByIdQuery, AnalizingTextDataBaseModel>(getReviewingTextByIdQuery);

            var formattedImageLink = textModel.ImageLink == null ? null : textModel.ImageLink.Link;
            var formattedSourceLink = textModel.SourceLink == null ? null : textModel.SourceLink.Link;
            var formattedText = reviewingTextAnalizator.GetFormattedText(textModel.ContentText, textModel.HeaderText, formattedImageLink, formattedSourceLink);
            var shortText = reviewingTextAnalizator.GetShortText(formattedText);

            var titleExists = !string.IsNullOrWhiteSpace(textModel.HeaderText);
            var imageExists = textModel.ImageLinkId != null;
            var sourceLinkExists = textModel.SourceLinkId != null;

            return new ReviewingDataViewModel
            {
                Id = id,
                Title = textModel.HeaderText,
                TitleExists = titleExists,
                Text = formattedText,
                ImageExists = imageExists,
                LinkExists = sourceLinkExists,
                ShortText = shortText
            };
        }

        public void UpdateAnalizingDataHeader(long id, string header)
        {
            var getReviewingTextByIdQuery = new GetReviewingTextByIdQuery { Id = id };
            var textModel = queryDispatcher.Dispatch<GetReviewingTextByIdQuery, AnalizingTextDataBaseModel>(getReviewingTextByIdQuery);

            textModel.HeaderText = header;

            var updateAnalizingTextCommand = new UpdateAnalizingTextCommand {AnalizingText = textModel};
            commandDispatcher.Dispatch<UpdateAnalizingTextCommand, VoidCommandResponse>(updateAnalizingTextCommand);
        }

        public void UpdateAnalizingDataImageLink(long id, string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return;
            }

            var getReviewingTextByIdQuery = new GetReviewingTextByIdQuery { Id = id };
            var textModel = queryDispatcher.Dispatch<GetReviewingTextByIdQuery, AnalizingTextDataBaseModel>(getReviewingTextByIdQuery);

            link = reviewingTextAnalizator.GetFormattedImageLink(link);

            if (string.IsNullOrWhiteSpace(link))
            {
                return;
            }

            textModel.ImageLink = new ImageLinkDataBaseModel { Link = link };
            textModel.ImageLinkId = 0;

            var updateAnalizingTextCommand = new UpdateAnalizingTextCommand { AnalizingText = textModel };
            commandDispatcher.Dispatch<UpdateAnalizingTextCommand, VoidCommandResponse>(updateAnalizingTextCommand);
        }

        public void UpdateAnalizingDataSourceLink(long id, string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return;
            }

            var getReviewingTextByIdQuery = new GetReviewingTextByIdQuery { Id = id };
            var textModel = queryDispatcher.Dispatch<GetReviewingTextByIdQuery, AnalizingTextDataBaseModel>(getReviewingTextByIdQuery);

            link = reviewingTextAnalizator.GetFormattedSourceLink(link);

            if (string.IsNullOrWhiteSpace(link))
            {
                return;
            }

            textModel.SourceLink = new SourceLinkDataBaseModel { Link = link };
            textModel.SourceLinkId = 0;

            var updateAnalizingTextCommand = new UpdateAnalizingTextCommand { AnalizingText = textModel };
            commandDispatcher.Dispatch<UpdateAnalizingTextCommand, VoidCommandResponse>(updateAnalizingTextCommand); 
        }

        public void OpenGomelSatRedactor(string header, string shortText, string text)
        {
            enterGomelSatNewsEngine.Run(header, shortText, text);
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
                    Link = model.Link,
                    HeaderText = model.HeaderText
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
