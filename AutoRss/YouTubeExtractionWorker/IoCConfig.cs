using Autofac;
using AutoRss.Configuration;
using Microsoft.ServiceBus.Messaging;

namespace AutoRss.YouTubeExtractionWorker
{
    internal class IoCConfig
    {
        private const string TopicName = "autorss";
        private const string SubscriptionName = "youtube";

        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Configuration.Configuration>().As<IConfiguration>().SingleInstance();

            builder.Register(ctx =>
            {
                var connection = ctx.Resolve<IConfiguration>().MicrosoftServiceBusConnectionString;
                return SubscriptionClient.CreateFromConnectionString(connection, TopicName, SubscriptionName);
            }).As<SubscriptionClient>().SingleInstance();

            builder.Register(ctx =>
            {
                var connection = ctx.Resolve<IConfiguration>().MicrosoftServiceBusConnectionString;
                return TopicClient.CreateFromConnectionString(connection, TopicName);
            }).As<TopicClient>().SingleInstance();

            return builder.Build();
        }
    }
}
