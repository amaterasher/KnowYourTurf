using Alpinely.TownCrier;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Domain.Persistence;
using KnowYourTurf.Core.Domain.Tools;
using KnowYourTurf.Core.Html.FubuUI.HtmlConventionRegistries;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Web.Menus;
using KnowYourTurf.Web.Services;
using FubuMVC.UI;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Tags;
using MethodFitness.Core;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Rhino.Security.Interfaces;
using Rhino.Security.Services;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace KnowYourTurf.Web.Config
{
    public class KYTTestRegistry : Registry
    {
        public KYTTestRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.AssemblyContainingType(typeof (CoreLocalizationKeys));
                         x.AssemblyContainingType(typeof (MergedEmailFactory));
                         x.WithDefaultConventions();
                     });
            For<HtmlConventionRegistry>().Add<KnowYourTurfHtmlConventions>();
            For<IServiceLocator>().Singleton().Use(new StructureMapServiceLocator());
            For<IElementNamingConvention>().Use<KnowYourTurfElementNamingConvention>();
            For(typeof (ITagGenerator<>)).Use(typeof (TagGenerator<>));
            For<TagProfileLibrary>().Singleton();
            For<INHSetupConfig>().Use<NullNHSetupConfig>();

            For<ISessionFactoryConfiguration>().Singleton().Use<NullSqlServerSessionSourceConfiguration>();
            For<ISessionFactory>().Use<NullSessionFactory>();

            For<ISession>().Use<NullSession>();
            For<ISession>().Use<NullSession>().Named("NoFiltersOrInterceptor");


            For<IUnitOfWork>().Use<NullNHibernateUnitOfWork>();
            For<IUnitOfWork>().Add<NullNHibernateUnitOfWork>().Named("NoFiltersOrInterceptor");
            For<IUnitOfWork>().Add<NullNHibernateUnitOfWork>().Named("NoFilters");
            //For<IGetCompanyIdService>().Use<DataLoaderGetCompanyIdService>();

            For<IRepository>().Use<Repository>();
            For<IRepository>().Add(x => new Repository()).Named("NoFiltersOrInterceptor");

            For<ILocalizationDataProvider>().Use<LocalizationDataProvider>();
            For<IAuthenticationContext>().Use<WebAuthenticationContext>();
            For<IMenuConfig>().Use<MainMenu>();
            For<IMergedEmailFactory>().LifecycleIs(new UniquePerRequestLifecycle()).Use<MergedEmailFactory>();

            For<IAuthorizationService>().HybridHttpOrThreadLocalScoped().Use<AuthorizationService>();
            For<IAuthorizationRepository>().HybridHttpOrThreadLocalScoped().Use<AuthorizationRepository>();
            For<IPermissionsBuilderService>().HybridHttpOrThreadLocalScoped().Use<PermissionsBuilderService>();
            For<IPermissionsService>().HybridHttpOrThreadLocalScoped().Use<PermissionsService>();
            For(typeof (IGridBuilder<>)).Use(typeof (GridBuilder<>));
            For<ILogger>().Use(() => new NullLogger());


            // RegisterGrids();
        }
    }
}