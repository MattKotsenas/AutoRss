using System;
using System.Diagnostics;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using Autofac;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AutoRss.CloudSaverWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        private SubscriptionClient _consumer;
        private TopicClient _producer;
        private readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);
        private CloudSaver _saver;
        private IContainer _dependencyResolver;

        public override void Run()
        {
            Trace.TraceInformation("CloudSaver starting processing of messages");

            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            _consumer.OnMessage(receivedMessage =>
                {
                    try
                    {
                        Trace.TraceInformation("CloudSaver processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());

                        // TODO: Can we refactor this?
                        // TODO: Remove magic number
                        using (Observable.Interval(TimeSpan.FromMinutes(1)).Subscribe(_ => receivedMessage.RenewLock()))
                        {
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
                        }
                        receivedMessage.Complete();
                        Trace.TraceInformation("CloudSaver completed Service Bus message: " + receivedMessage.SequenceNumber.ToString());
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError("CloudSaver error processing message {0}. Error {1}", receivedMessage.SequenceNumber, e);
                        receivedMessage.Abandon();
                    }
                });

            _completedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            _dependencyResolver = (new IoCConfig()).BuildContainer();

            _consumer = _dependencyResolver.Resolve<SubscriptionClient>();
            _producer = _dependencyResolver.Resolve<TopicClient>();
            _saver = _dependencyResolver.Resolve<CloudSaver>();

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
