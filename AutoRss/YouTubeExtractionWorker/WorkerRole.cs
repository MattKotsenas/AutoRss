using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AutoRss.YouTubeExtractionWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        private const string TopicName = "autorss";
        private const string SubscriptionName = "youtube";

        private SubscriptionClient _consumer;
        private TopicClient _producer;
        private readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);

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
                        var extractor = new YouTubeUrlExtractor();
                        var item = extractor.Download(receivedMessage.Properties["Url"].ToString());

                        var message = new BrokeredMessage();
                        message.Properties["Action"] = "Write";
                        message.Properties["Url"] = item.Url;
                        message.Properties["Name"] = item.Name;

                        _producer.Send(message);
                        receivedMessage.Complete();
                    }
                    catch (Exception exception)
                    {
                        // Handle any message processing specific exceptions here
                        Trace.TraceError("Error processing message {0}", exception);
                    }
                });

            _completedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.SubscriptionExists(TopicName, SubscriptionName))
            {
                namespaceManager.CreateSubscription(TopicName, SubscriptionName,
                    new SqlFilter("[Action] = 'Extract' AND [Url] LIKE '%youtube.com%'"));
            }

            _consumer = SubscriptionClient.CreateFromConnectionString(connectionString, TopicName, SubscriptionName);
            _producer = TopicClient.CreateFromConnectionString(connectionString, TopicName);

            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            _consumer.Close();
            _producer.Close();
            _completedEvent.Set();
            base.OnStop();
        }
    }
}
