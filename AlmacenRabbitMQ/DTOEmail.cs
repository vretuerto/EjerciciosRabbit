using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmacenRabbitMQ
{
    internal class DTOEmail
    {
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void Send()
        {
           Console.WriteLine("Mail envíado a " +  EmailAddress);
        }
    }
}
