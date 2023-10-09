using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.EntityFramework;
using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _MessageDal;

        public MessageManager(IMessageDal MessageDal)
        {

            _MessageDal = MessageDal;
        }

        public async Task<List<Message>> GetMessagesForReceiver(string userName)
        {
            var list = await _MessageDal.GetListAsync();
            var finalList = list.Where(x=>x.isDeletedforReceiver==false && x.ReceiverUserName==userName).ToList();
            return finalList;

        }

        public async Task<List<Message>> GetMessagesForSender(string userName)
        {
            var list = await _MessageDal.GetListAsync();
            var finalList = list.Where(x => x.isDeletedforSender == false && x.SenderUserName == userName).ToList();
            return finalList;
        }

        public async Task TDeleteAsync(Message t)
        {
            await _MessageDal.DeleteAsync(t);
        }

        public async Task<Message> TGetByIdAsync(int id)
        {
            return await _MessageDal.GetByIdAsync(id);
        }

        public async Task<List<Message>> TGetListAsync()
        {
            return await _MessageDal.GetListAsync();
        }

        public async Task<List<Message>> TGetListByFilter(Expression<Func<Message, bool>> filter)
        {
            return await _MessageDal.GetListByFilter(filter);
        }

        public async Task TInsertAsync(Message t)
        {
            await _MessageDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Message t)
        {
            await _MessageDal.UpdateAsync(t);
        }
    }
}
