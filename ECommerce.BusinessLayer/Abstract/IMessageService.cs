using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        Task<List<Message>> GetMessagesForSender(string userName);
        Task<List<Message>> GetMessagesForReceiver(string userName);
    }
}
