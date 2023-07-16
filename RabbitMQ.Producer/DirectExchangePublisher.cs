using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare(exchange: "ex.direct", type: ExchangeType.Direct);

            var message = new { Name = "Producer", Message = "Hello!" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: "ex.direct", routingKey: "myBindingKey", basicProperties: null, body: body);
        }
    }
}