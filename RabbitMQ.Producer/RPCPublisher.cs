using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class RPCPublisher
    {
        public static void Publish(IModel channel)
        {
            var queueName = "rpc-queue";

            var replyQueue = channel.QueueDeclare(string.Empty, exclusive: true);
            channel.QueueDeclare(queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);            

            var consumer = new EventingBasicConsumer(channel);

            Console.WriteLine(" [x] Awaiting RPC requests");

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Reply Received: {message}");                
            };

            channel.BasicConsume(queue: replyQueue.QueueName, autoAck: true, consumer: consumer);

            for (int i = 0; i <= 5; i++)
            {
                var properties = channel.CreateBasicProperties();
                properties.ReplyTo = replyQueue.QueueName;
                properties.CorrelationId = Guid.NewGuid().ToString();

                var message = $"Hi reply message number {i}";
                var body = Encoding.UTF8.GetBytes(message);

                Console.WriteLine($"Sending Request: {properties.CorrelationId}");

                channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: properties, body: body);
                int dots = message.Split('.').Length - 1;
                Thread.Sleep(dots * 1000);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}