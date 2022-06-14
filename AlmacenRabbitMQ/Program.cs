using AlmacenRabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

IConnection connection;
IModel channel;

// Creamos conexión
ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5672;
factory.UserName = "guest";
factory.Password = "guest";


connection = factory.CreateConnection();
channel = connection.CreateModel();
// Creamos canal
var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;
channel.BasicConsume("mails", true, consumer);

Console.WriteLine("Esperando mails...");
Console.ReadKey();

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    string Mail = Encoding.UTF8.GetString(e.Body.ToArray());
    DTOEmail response = JsonConvert.DeserializeObject<DTOEmail>(Mail);
    response.Send();
    Console.WriteLine("Email: " + Mail);
}