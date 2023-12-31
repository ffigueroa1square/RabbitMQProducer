﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class RoutingPublisher
    {
        public static void Publish(IModel channel)
        {
            var exchangeName = "ex.directRouted";
            var routingKeys = new string[] { "orange", "blue" };

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

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