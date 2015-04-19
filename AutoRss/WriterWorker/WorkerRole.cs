﻿using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using AutoRss.Models;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AutoRss.WriterWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        private const string TopicName = "autorss";
        private const string SubscriptionName = "writer";
        private  SubscriptionClient _consumer;
        readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);

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
                        var repo = new MediaRepository();
                        repo.Create(new MediaItem
                        {
                            Created = DateTime.Now,
                            DownloadLink = receivedMessage.Properties["Url"].ToString(),
                            MimeType = "video/mp4",
                            Name = receivedMessage.Properties["Name"].ToString(),
                            Size = 0
                        });
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

            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.SubscriptionExists(TopicName, SubscriptionName))
            {
                namespaceManager.CreateSubscription(TopicName, SubscriptionName, new SqlFilter("[Action] = 'Write'"));
            }

            _consumer = SubscriptionClient.CreateFromConnectionString(connectionString, TopicName, SubscriptionName);
            return base.OnStart();
        }

        public override void OnStop()
        {
            _consumer.Close();
            _completedEvent.Set();
            base.OnStop();
        }
    }
}