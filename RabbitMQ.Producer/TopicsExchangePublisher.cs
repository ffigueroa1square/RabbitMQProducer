using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class TopicsExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var exchangeName = "ex.topicsRouted";
            var routingKeys = new string[] { 
                "quick.orange.rabbit", 
                "lazy.orange.elephant", 
                "quick.orange.fox", 
                "lazy.brown.fox", 
                "lazy.pink.rabbit", 
                "quick.brown.fox", 
                "orange", 
                "quick.orange.new.rabbit", 
                "lazy.orange.new.rabbit" };

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

            foreach (var route in routingKeys)
            {
                var message = $"this a message for {route} route";

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: exchangeName, routingKey: route, basicProperties: null, body: body);
                Console.WriteLine($" [x] Sent '{route}':'{message}'");
                Thread.Sleep(1000);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}