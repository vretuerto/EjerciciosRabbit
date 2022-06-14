using Microsoft.AspNetCore.Mvc;
using WebAPIRabbitMQ.DTOs;
using WebAPIRabbitMQ.Services;

namespace WebAPIRabbitMQ.Controllers
{
    [ApiController]
    [Route("mails")]
    public class RabbitMQController : ControllerBase
    {
        private readonly IEmailProducer messagePublisher;

        public RabbitMQController(IEmailProducer messagePublisher)
        {
            this.messagePublisher = messagePublisher;
        }

        [HttpPost]
        public async Task<ActionResult<DTOEmail>> PostEmail(DTOEmail mail)
        {
            DTOEmail newMail = new DTOEmail()
            {
                EmailAddress = mail.EmailAddress,
                Subject = mail.Subject,
                Body = mail.Body
            };

            messagePublisher.SendMessage(newMail);

            return Ok();
        }


    }
}
