using Microsoft.ServiceBus.Messaging;
using System;

namespace HSRQueueTest
{
    class Program
    {
        const string _CONNSTR = "Endpoint=sb://hsrqueuetest.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=HSO9jNLER3sV88XC8aEKYzfCokweVoShu9XgyfNABlQ=";
        const string _QUEUENAME = "queuetest_1";
        const string _TOPICNAME = "topictest_1";

        static void Main(string[] args)
        {
            //SendMessageToQueue();
            //Console.WriteLine("----------------------------------------------------");

            //Console.WriteLine("");

            //ReceiveMessageFromQueue();
            //Console.WriteLine("----------------------------------------------------");
            //Console.WriteLine("");

            SendMessageToTopic();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("");

            ReceiveMessageFromTopic();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("");

            Console.WriteLine("Message sucessfully sent! Press ENTER to exit program");

            Console.ReadLine();
        }

        static void SendMessageToQueue()
        {
            Console.WriteLine("Sending messaging");
            var client = QueueClient.CreateFromConnectionString(_CONNSTR, _QUEUENAME);

            for (int i = 0; i < 1000; i++)
            {
                var message = new BrokeredMessage($"This is a {i} test message!");
                Console.WriteLine($"Message id: {message.MessageId}");
                client.Send(message);
            }
        }

        static void ReceiveMessageFromQueue()
        {
            Console.WriteLine("Receiving messaging");

            var client = QueueClient.CreateFromConnectionString(_CONNSTR, _QUEUENAME);

            client.OnMessage(message =>
            {
                Console.WriteLine($"Message body: {message.GetBody<String>()}");
                Console.WriteLine($"Message id: {message.MessageId}");
            });
        }

        static void SendMessageToTopic()
        {
            try
            {
                var client = TopicClient.CreateFromConnectionString(_CONNSTR, _TOPICNAME);
                for (int i = 0; i < 100; i++)
                {
                    var message = new BrokeredMessage($"This is a {i} test message!");
                    Console.WriteLine($"Message id: {message.MessageId}");
                    client.Send(message);

                    //Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>())); 
                }

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        static void ReceiveMessageFromTopic()
        {
            try
            {
                var client = SubscriptionClient.CreateFromConnectionString(_CONNSTR, _TOPICNAME, "subscription_topictest_1");

                client.OnMessage(message =>
                {
                    Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
                    Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
                });
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
