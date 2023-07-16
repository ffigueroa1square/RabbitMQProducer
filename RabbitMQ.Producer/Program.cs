using RabbitMQ.Client;
using RabbitMQ.Producer;

internal static class Program
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        /*
        // uncomment the rabbitmq pattern you want to use
        */

        SimpleQueuePublisher.Publish(channel);
        //DirectExchangePublisher.Publish(channel);
    }
}