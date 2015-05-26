using Autofac;
using AutoRss.Configuration;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AutoRss.CloudSaverWorker
{
    internal class IoCConfig
    {
        private const string TopicName = "autorss";
        private const string SubscriptionName = "cloudsave";
        private const string CloudSaveContainerName = "cloudsave";

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

            builder.Register(ctx =>
            {
                var connection = ctx.Resolve<IConfiguration>().CloudSaverStorageConnectionString;
                var storage = CloudStorageAccount.Parse(connection);
                var client = storage.CreateCloudBlobClient();
                return client.GetContainerReference(CloudSaveContainerName);
            }).AsSelf().SingleInstance();

            builder.Register(ctx => new CloudSaver(ctx.Resolve<CloudBlobContainer>())).AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
