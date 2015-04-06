using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoRss.Models;
using AutoRss.Models.Syndication;
using AutoRss.SyndicationFeedFormatter;

namespace AutoRss.SyndicationWebRole
{
    public class IoCConfig
    {
        public static void RegisterIoC(HttpConfiguration configuration)
        {
            var builder = BuildContainer(new ContainerBuilder());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static ContainerBuilder BuildContainer(ContainerBuilder builder)
        {
            builder.Register(ctx => new MediaItemToSyndicationItemMapper("AutoRss", "AutoRss Media"))
                .As<ISyndicationFeedMapper>().SingleInstance();

            builder.RegisterType<MediaRepository>().As<IReadOnlyRepository<MediaItem>>().SingleInstance();

            return builder;
        }
    }
}