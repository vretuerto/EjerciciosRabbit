using WebAPIRabbitMQ.DTOs;

namespace WebAPIRabbitMQ.Services
{
    public interface IEmailProducer
    {
        void SendMessage(DTOEmail message);
    }
}
