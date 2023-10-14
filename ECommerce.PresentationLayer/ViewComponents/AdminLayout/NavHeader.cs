using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Messages;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Models;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents.AdminLayout
{
    public class NavHeader : ViewComponent
    {
        private readonly IApiService _apiService;

        public NavHeader(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Mesaj bildirimleri
            string url = "https://localhost:7175/api/AdminMessage/GetAllMessagesForReceiver?userName=" + User.Identity.Name;
            var values = await _apiService.GetTableData<ResultMessageDto>(url);

            List<NewMessageNotificationViewModel> notification = new();

            foreach (var row in values)
            {
                NewMessageNotificationViewModel viewModel = new()
                {
                    Body = row.Body,
                    Created = row.Created,
                    isDeletedforReceiver = row.isDeletedforReceiver,
                    isDeletedforSender = row.isDeletedforSender,
                    isReadForReceiver = row.isReadForReceiver,
                    MessageId = row.MessageId,
                    SenderUser = await _apiService.GetData<AppUser>("https://localhost:7175/api/User/GetOneUser/" + row.SenderUserName),
                    ReceiverUserName = row.ReceiverUserName,
                    SenderUserName = row.SenderUserName,
                    Subject = row.Subject
                };
                notification.Add(viewModel);

            }

            var nonReadMessages = values.Where(x => x.isReadForReceiver == false).ToList();



            ViewBag.InboxCount = nonReadMessages.Count().ToString();

            //Kullanıcı bilgisi
            string urlForUser = "https://localhost:7175/api/Admin/GetOneAdmin/" + User.Identity.Name;
            var user = await _apiService.GetData<ResultUserDto>(urlForUser);
            if (!string.IsNullOrEmpty(user.ImageUrl))
            {
                ViewBag.UserImage = user.ImageUrl;
            }
            else
            {
                ViewBag.UserImage = "https://upload.wikimedia.org/wikipedia/commons/2/2c/Default_pfp.svg";
            }


            return View(notification);
        }
    }
}
