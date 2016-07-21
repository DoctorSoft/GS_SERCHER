using CommandsAndQueries.CommandsAndHandlers.AnalizingTexts;
using CommandsAndQueries.CommandsAndHandlers.GomelSatNews;
using CommandsAndQueries.CommandsAndHandlers.RequestRecords;
using CommandsAndQueries.CommandsAndHandlers.Words;
using CommandsAndQueries.Dispatchers;
using CommandsAndQueries.QueriesAndHandlers.AnalizingTexts;
using CommandsAndQueries.QueriesAndHandlers.GomelSatNews;
using CommandsAndQueries.QueriesAndHandlers.GomelSatSiteLinks;
using CommandsAndQueries.QueriesAndHandlers.RequestRecords;
using CommandsAndQueries.QueriesAndHandlers.Words;
using Common.CommandQueryTools;
using DataBase.Contexts;
using DataParsers;
using DataParsers.Models;
using DataParsers.NewsParsers;
using DataParsers.WordParsers;
using DataProviders;
using DataProviders.SiteDataPrividers;
using DataProviders.WordsDataProviders;
using FilesManagers.WordFileManagers;
using Ninject.Modules;
using Ninject.Web.Common;
using Services.GomelSat;
using Services.Words;
using TextAnalizators;

namespace NinjectModules
{
    public class MainNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DataBaseContext>().To<DataBaseContext>().InRequestScope();

            Bind<ICommandDispatcher>().To<CommandDispatcher>().InRequestScope();
            Bind<IQueryDispatcher>().To<QueryDispatcher>().InRequestScope();

            //// Request controller
            Bind<object>().To<GetLastRequestDateQueryHandler>().WhenInjectedInto<QueryDispatcher>().InRequestScope();
            Bind<object>().To<UpdateLastRequestDateCommandHandler>().WhenInjectedInto<CommandDispatcher>().InRequestScope();

            //// Text analyze
            Bind<object>().To<AddAnalizingTextCommandHandler>().WhenInjectedInto<CommandDispatcher>().InRequestScope();
            Bind<object>().To<GetAnalizingTextByIdQueryHandler>().WhenInjectedInto<QueryDispatcher>().InRequestScope();

            //// Gomel sat service
            Bind<ISiteDataProvider>().To<GomelSatDataProvider>().WhenInjectedInto<GomelSatService>().InRequestScope();
            Bind<ISiteNewsHeadersParser<GomelSatNewsHeaderModel>>().To<GomelSatNewsHeadersParser>().WhenInjectedInto<GomelSatService>().InRequestScope();
            Bind<ISiteNewsContentParser<GomelSatNewsContentModel>>().To<GomelSatNewsContentParser>().WhenInjectedInto<GomelSatService>().InRequestScope();
            Bind<IGomelSatService>().To<GomelSatService>().InRequestScope();
            Bind<ITextAnalizator<GomelSatNewsModel>>().To<GomelSatTextAnalizator>().WhenInjectedInto<GomelSatService>().InRequestScope();

            Bind<object>().To<SynchronizeGomelSatNewsCommandHandler>().WhenInjectedInto<CommandDispatcher>().InRequestScope();
            Bind<object>().To<GetGomelSatNewsQueryHandler>().WhenInjectedInto<QueryDispatcher>().InRequestScope();
            Bind<object>().To<GetGomelSatNewsWithoutContentQueryHandler>().WhenInjectedInto<QueryDispatcher>().InRequestScope();
            Bind<object>().To<AddGomelSatNewsContentsCommandHandler>().WhenInjectedInto<CommandDispatcher>().InRequestScope();
            Bind<object>().To<RemoveLastGomelSatNewsCommandHandler>().WhenInjectedInto<CommandDispatcher>().InRequestScope();
            Bind<object>().To<GetGomelSatSiteLinksQueryHandler>().WhenInjectedInto<QueryDispatcher>().InRequestScope();

            //// Words service
            Bind<IWordService>().To<WordService>().InRequestScope();
            Bind<IWordFormsDataParser>().To<WordFormsDataParser>().InRequestScope();
            Bind<IWordFormsProvider>().To<WordFormsProvider>().InRequestScope();
            Bind<IWordFileManager>().To<WordFileManager>().InRequestScope();

            Bind<object>().To<GetWordListQueryHandler>().WhenInjectedInto<QueryDispatcher>().InRequestScope();
            Bind<object>().To<AddWordCommandHandler>().WhenInjectedInto<CommandDispatcher>().InRequestScope();
            Bind<object>().To<GetLastAddedWordListQueryHandler>().WhenInjectedInto<QueryDispatcher>().InRequestScope();
            Bind<object>().To<RemoveWordListCommandHandler>().WhenInjectedInto<CommandDispatcher>().InRequestScope();
            Bind<object>().To<WordCanBeAddedQueryHandler>().WhenInjectedInto<QueryDispatcher>().InRequestScope();
        }
    }
}
