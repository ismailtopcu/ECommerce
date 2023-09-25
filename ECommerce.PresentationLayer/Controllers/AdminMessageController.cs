using ECommerce.DtoLayer.Dtos.Messages;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminMessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiService _apiService;

        public AdminMessageController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [Route("adminpanel/messages/inbox")]
        public async Task<IActionResult> Inbox()
        {
            string url = "https://localhost:7175/api/AdminMessage/GetAllMessagesForReceiver?userName=" + User.Identity.Name.ToString();
            var values = await _apiService.GetTableData<ResultMessageDto>(url);

            ViewBag.InboxCount = values.Count().ToString();
            return View(values);
        }

        [Route("adminpanel/messages/sendbox")]
        public async Task<IActionResult> SendBox() 
        {
            string url = "https://localhost:7175/api/AdminMessage/GetAllMessagesForSender?userName=" + User.Identity.Name.ToString();
            var value = await _apiService.GetTableData<ResultMessageDto>(url);
            ViewBag.SendboxCount = value.Count.ToString();
            return View(value);
        }

        [Route("adminpanel/messages/sendmessage/{userName}")]
        [HttpGet]
        public IActionResult NewMessage(string? userName)
        {
            if (userName != null)
            {
                CreateMessageDto messageDto = new CreateMessageDto();
                messageDto.ReceiverUserName = userName;
            }
            return View();
        }
        [Route("adminpanel/messages/sendmessage/{userName}")]
        [HttpPost]
        public async Task<IActionResult> NewMessage(CreateMessageDto createMessage) 
        {
            string url = "https://localhost:7175/api/AdminMessage";
            await _apiService.AddData(url, createMessage);
            return RedirectToAction("SendBox");
        }

        [Route("adminpanel/messages/detail/{id}")]
        public async Task<IActionResult> MessageDetailForReceiver(int id) 
        {
            string url = "https://localhost:7175/api/AdminMessage/GetMessageByIdReceiver/" + id;
            var value = await _apiService.GetData<ResultMessageDto>(url);
            return View(value);
        }

        [Route("adminpanel/messages/sendmessagedetail/{id}")]
        public async Task<IActionResult> MessageDetailForSender(int id)
        {
            string url = "https://localhost:7175/api/AdminMessage/GetMessageByIdSender/" + id;
            var value = await _apiService.GetData<ResultMessageDto>(url);
            return View(value);
        }

        public IActionResult SendMail() 
        {
            return View();
        }
    }
}
