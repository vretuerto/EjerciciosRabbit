using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using WebAPIRabbitMQ.DTOs;

namespace WebAPIRabbitMQ.Services
{
    public class RabbitMQEmailProducer : IEmailProducer
    {
        public void SendMessage(DTOEmail message)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            factory.Port = 5672;
            factory.UserName = "guest";
            factory.Password = "guest";

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("ex.mails", "direct", true, false);
            channel.QueueDeclare("mails", true, false, false, null);
            channel.QueueBind("mails", "ex.mails", "mails");

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "ex.mails", routingKey: "mails", body: body);
        }
    }
}
