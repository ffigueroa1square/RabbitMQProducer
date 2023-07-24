using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class HeadersExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var exchangeName = "ex.headersRouted";
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Headers);

            var headersQueue1 = new Dictionary<string, object>();
            headersQueue1.Add("categoria", "vertebrados");
            headersQueue1.Add("tipo", "mamifero");

            SendMensagge(exchangeName, channel, headersQueue1, "hola vertebrados mamiferos");

            var headersQueue2 = new Dictionary<string, object>();
            headersQueue2.Add("categoria", "vertebrados");
            headersQueue2.Add("tipo", "ave");

            SendMensagge(exchangeName, channel, headersQueue2, "hola vertebrados aves");

            var headersQueue3 = new Dictionary<string, object>();
            headersQueue3.Add("categoria", "invertebrados");
            headersQueue3.Add("tipo", "ave");

            SendMensagge(exchangeName, channel, headersQueue3, "hola invertebrados aves");

            var headersQueue4 = new Dictionary<string, object>();
            headersQueue4.Add("categoria", "vertebrados");
            headersQueue4.Add("tipo", "pulpos");

            SendMensagge(exchangeName, channel, headersQueue4, "hola vertebrados pulpos");

            var headersQueue5 = new Dictionary<string, object>();
            headersQueue5.Add("categoria", "invertebrados");
            headersQueue5.Add("tipo", "pulpos");

            SendMensagge(exchangeName, channel, headersQueue5, "hola invertebrados pulpos");

            var headersQueue6 = new Dictionary<string, object>();
            headersQueue6.Add("categoria", "mixto");
            headersQueue6.Add("tipo", "aliens");

            SendMensagge(exchangeName, channel, headersQueue6, "hola mixtos aliens");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static void SendMensagge(string exchangeName, IModel channel, Dictionary<string,object> header, string message)
        {
            var properties = channel.CreateBasicProperties();
            properties.Headers = header;

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: exchangeName, routingKey: string.Empty, basicProperties: properties, body: body);
            Console.WriteLine($" [x] Sent categoria: '{header["categoria"]}' tipo: '{header["tipo"]}' mensaje: '{message}'");
            Thread.Sleep(1000);
        }
    }
}