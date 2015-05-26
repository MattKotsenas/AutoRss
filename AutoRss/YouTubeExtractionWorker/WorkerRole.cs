using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Autofac;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AutoRss.YouTubeExtractionWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        private ILifetimeScope _container;
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
                        message.Properties["Action"] = "CloudSave";
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

            _container = (new IoCConfig()).BuildContainer().BeginLifetimeScope();
            _consumer = _container.Resolve<SubscriptionClient>();
            _producer = _container.Resolve<TopicClient>();

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
