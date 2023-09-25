using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Messages;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents.AdminLayout
{
    public class NavHeader : ViewComponent
    {
        private readonly ApiService _apiService;

        public NavHeader(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Mesaj bildirimleri
            string url = "https://localhost:7175/api/AdminMessage/GetAllMessagesForReceiver?userName=" + User.Identity.Name;
            var values = await _apiService.GetTableData<ResultMessageDto>(url);
            var nonReadMessages = values.Where(x => x.isReadForReceiver == false).ToList();
            ViewBag.InboxCount = values.Count().ToString();
            
            //Kullanıcı bilgisi
            string urlForUser = "https://localhost:7175/api/Admin/GetOneAdmin/" + User.Identity.Name;
            var user = await _apiService.GetData<ResultUserDto>(urlForUser);
            ViewBag.UserImage = user.ImageUrl;


            return View(nonReadMessages);
        }
    }
}
