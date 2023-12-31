﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var exchangeName = "ex.fanout";

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: exchangeName, routingKey: string.Empty, basicProperties: null, body: body);
                Console.WriteLine($" [x] Sent {message}");
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}