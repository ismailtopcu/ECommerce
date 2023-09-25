using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.Concrete
{
    public class Message
    {
        public int MessageId { get; set; }
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public bool isDeletedforReceiver { get; set; } = false;
        public bool isDeletedforSender { get; set; } = false;
        public bool isReadforReceiver { get; set; } = false;

    }
}
