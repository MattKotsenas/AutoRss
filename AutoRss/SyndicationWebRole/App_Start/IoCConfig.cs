using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoRss.Models;
using AutoRss.Models.Mocks;
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
            builder.RegisterType<Configuration.Configuration>().As<IConfiguration>().SingleInstance();

            builder.Register(ctx => new MediaItemToSyndicationItemMapper("AutoRss", "AutoRss Media"))
                .As<ISyndicationFeedMapper>().SingleInstance();

            builder.Register(ctx =>
            {
                var config = ctx.Resolve<IConfiguration>();
                if (config.UseMockMediaRepository)
                {
                    return (IReadOnlyRepository<MediaItem>)new MockMediaRepository();
                }
                return new MediaRepository(config.MediaRepositoryConnectionString);
            }).As<IReadOnlyRepository<MediaItem>>().SingleInstance();

            return builder;
        }
    }
}