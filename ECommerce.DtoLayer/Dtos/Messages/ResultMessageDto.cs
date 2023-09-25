using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.Messages
{
    public class ResultMessageDto
    {
        public int MessageId { get; set; }
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; } 
        public bool isDeletedforReceiver { get; set; } 
        public bool isDeletedforSender { get; set; } 
        public bool isReadForReceiver { get; set; } 
    }
}
