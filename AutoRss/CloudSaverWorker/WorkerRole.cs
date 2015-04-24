using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace AutoRss.CloudSaverWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        private const string TopicName = "autorss";
        private const string SubscriptionName = "cloudsave";

        private SubscriptionClient _consumer;
        private TopicClient _producer;
        private readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);
        private CloudSaver _saver;

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            _consumer.OnMessage(receivedMessage =>
                {
                    try
                    {
                        // Process the message
                        Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());
                        var uri = new Uri(receivedMessage.Properties["Url"].ToString());
                        var name = receivedMessage.Properties["Name"].ToString();

                        var result = _saver.Save(uri, name);
                        var message = new BrokeredMessage();
                        message.Properties["Name"] = name;
                        message.Properties["Url"] = result.Item1.AbsoluteUri;
                        message.Properties["MimeType"] = result.Item2;
                        message.Properties["Size"] = result.Item3;
                        message.Properties["Action"] = "Write";
                        _producer.Send(message);
                        receivedMessage.Complete();
                    }
                    catch
                    {
                        // Handle any message processing specific exceptions here
                    }
                });

            _completedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // TODO: Swap all CloudConfigurationManagers with IConfiguration
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.SubscriptionExists(TopicName, SubscriptionName))
            {
                namespaceManager.CreateSubscription(TopicName, SubscriptionName,
                    new SqlFilter("[Action] = 'CloudSave'"));
            }

            _consumer = SubscriptionClient.CreateFromConnectionString(connectionString, TopicName, SubscriptionName);
            _producer = TopicClient.CreateFromConnectionString(connectionString, TopicName);

            var blobConnectionString = CloudConfigurationManager.GetSetting("CloudSaver.StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(blobConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("cloudsave");

            _saver = new CloudSaver(blobContainer);

            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            _consumer.Close();
            _completedEvent.Set();
            base.OnStop();
        }
    }
}
