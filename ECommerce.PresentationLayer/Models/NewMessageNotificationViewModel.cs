using ECommerce.EntityLayer.Concrete;

namespace ECommerce.PresentationLayer.Models
{
    public class NewMessageNotificationViewModel
    {
        public int MessageId { get; set; }
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public AppUser SenderUser { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool isDeletedforReceiver { get; set; }
        public bool isDeletedforSender { get; set; }
        public bool isReadForReceiver { get; set; }
    }
}
