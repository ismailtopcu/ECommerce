using ECommerce.DtoLayer.Dtos.Messages;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminMessageController : Controller
    {
        private readonly IApiService _apiService;
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminMessageController(IApiService apiService, IHttpClientFactory httpClientFactory)
        {
            _apiService = apiService;
            _httpClientFactory = httpClientFactory;
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
                return View(messageDto);

            }
            return View();
        }
        [Route("adminpanel/messages/sendmessage/{userName}")]
        [HttpPost]
        public async Task<IActionResult> NewMessage(CreateMessageDto createMessage)
        {

            string url = "https://localhost:7175/api/AdminMessage";
            createMessage.SenderUserName = User.Identity.Name;
            createMessage.Created = DateTime.Now;
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

        [Route("adminpanel/messages/deletemessage/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            string url = "https://localhost:7175/api/AdminMessage/GetMessageByIdSender/" + id;
            var value = await _apiService.GetData<ResultMessageDto>(url);
            if (value.SenderUserName == User.Identity.Name)
            {
                string senderUrl = "https://localhost:7175/api/AdminMessage/DeleteMessage/" + id + ",false";
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync(senderUrl);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Inbox");
                }
            }

            if (value.ReceiverUserName == User.Identity.Name)
            {
                string receiverUrl = "https://localhost:7175/api/AdminMessage/DeleteMessage/" + id + ",true";
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync(receiverUrl);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Inbox");
                }
            }

            return RedirectToAction("Inbox");

        }

        [Route("adminpanel/messages/sendemail")]
        [HttpGet]
        public IActionResult SendMail()
        {
            return View();
        }
        [Route("adminpanel/messages/sendemail")]
        [HttpPost]
        public async Task<IActionResult> SendMail(CreateMailDto createMailDto)
        {
            if (ModelState.IsValid)
            {
                string url = "https://localhost:7175/api/AdminMessage/SendEmail";
                await _apiService.AddData(url, createMailDto);
                return RedirectToAction("Sendbox");
            }
            return View(createMailDto);

        }
    }
}
