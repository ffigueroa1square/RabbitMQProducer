using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class SimpleQueuePublisher
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("demo-queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
            var message = new { Name = "Producer", Message = "Hello!" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "demo-queue", null, body);
        }
    }
}