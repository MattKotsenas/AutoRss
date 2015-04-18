using System;
using System.Linq;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace AutoRss.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = args.FirstOrDefault();
            var url = args.Skip(1).FirstOrDefault();

            if (String.IsNullOrWhiteSpace(connectionString))
            {
                System.Console.Write("Enter connection string: ");
                connectionString = System.Console.ReadLine();
                System.Console.WriteLine();
            }

            if (String.IsNullOrWhiteSpace(url))
            {
                System.Console.Write("Enter Url to extract: ");
                url = System.Console.ReadLine();
                System.Console.WriteLine();
            }

            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            var producer = TopicClient.CreateFromConnectionString(connectionString, "autorss");

            var message = new BrokeredMessage();
            message.Properties["Url"] = url;
            message.Properties["Action"] = "Extract";
            producer.Send(message);
        }
    }
}
