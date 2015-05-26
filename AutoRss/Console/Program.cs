using Microsoft.ServiceBus.Messaging;

namespace AutoRss.Console
{
    class Program
    {
        private static void Main()
        {
            System.Console.Write("Enter connection string: ");
            var connectionString = System.Console.ReadLine();
            System.Console.WriteLine();

            var producer = TopicClient.CreateFromConnectionString(connectionString, "autorss");

            string url;
            while ((url = GetInput()) != "exit")
            {
                var message = new BrokeredMessage();
                message.Properties["Url"] = url;
                message.Properties["Action"] = "Extract";
                producer.Send(message);
            }
        }

        private static string GetInput()
        {
            System.Console.Write("Enter Url to extract (type 'exit' to quit): ");
            return System.Console.ReadLine();
        }
    }
}
