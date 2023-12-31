﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var exchangeName = "ex.direct";
            var routingKey = "myBindingKey";

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

            var message = new { Name = "Producer", Message = "Hello!" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);

            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}