using Autofac;
using AutoRss.Models;
using AutoRss.Models.Mocks;

namespace AutoRss.WriterWorker
{
    internal class IoCConfig
    {
        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Configuration.Configuration>().As<IConfiguration>().SingleInstance();

            builder.Register(ctx =>
            {
                var config = ctx.Resolve<IConfiguration>();
                if (config.UseMockMediaRepository)
                {
                    return (IReadWriteRepository<MediaItem>) new MockMediaRepository();
                }
                return new MediaRepository(config.MediaRepositoryConnectionString);
            }).As<IReadOnlyRepository<MediaItem>>()
              .As<IWriteRepository<MediaItem>>()
              .SingleInstance();

            builder.RegisterType<MediaWriter>().AsSelf();

            return builder.Build();
        }
    }
}
