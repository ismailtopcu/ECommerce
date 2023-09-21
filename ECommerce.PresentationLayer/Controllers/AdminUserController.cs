using ECommerce.DtoLayer.Dtos.AccountDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.PresentationLayer.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> UserList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/User/GetAllUsers");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7175/api/User/DeleteUser/"+userName);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("UserList", "AdminUser");
            }
            return View();
        }
    }
}
