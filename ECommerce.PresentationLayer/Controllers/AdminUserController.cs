using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using X.PagedList;

namespace ECommerce.PresentationLayer.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //Üyeler sekmesi
        [Route("adminpanel/userlist")]
        public async Task<IActionResult> UserList(int page = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/User/GetAllMembers");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
                return View(values.ToPagedList(page,10));
            }
            return View();
        }

        [Route("adminpanel/deleteuser/{userName}")]
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

        [HttpGet]
        [Route("adminpanel/userdetail/{userName}")]
        public IActionResult UserDetail(string userName)
        {
            ViewBag.userName = userName;

            return View();
        }


        //Admin Sekmesi
        [Route("adminpanel/adminlist")]
        public async Task<IActionResult> AdminList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Admin/GetAllAdmins");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        [Route("adminpanel/createadmin")]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        [Route("adminpanel/createadmin")]
        public async Task<IActionResult> CreateAdmin(CreateNewUserDto createNewUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createNewUserDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var respMessage = await client.PostAsync("https://localhost:7175/api/Admin/CreateAdmin", content);
            if (respMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminList");
            }
            return View();
        }

        [HttpGet]
        [Route("adminpanel/settings/profile")]
        public async Task<IActionResult> EditAdmin()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Admin/GetOneAdmin/" + User.Identity.Name);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateUserDto>(jsonData);
                return View(values);
            }
            return RedirectToAction("AdminList");
        }
        [HttpPost]
        [Route("adminpanel/settings")]
        public async Task<IActionResult> EditAdmin(UpdateUserDto updateUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUserDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7175/api/Admin/UpdateAdmin", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminList");
            }
            return View();
        }

        [Route("adminpanel/deleteadmin/{userName}")]
        public async Task<IActionResult> DeleteAdmin(string userName)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7175/api/User/DeleteUser/" + userName);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminList", "AdminUser");
            }
            return View();
        }

        [HttpGet]
        [Route("adminpanel/settings/password")]
        public IActionResult ChangePassword() 
        {
            return View();
        }
        [HttpPost]
        [Route("adminpanel/settings/password")]
        public async Task<IActionResult> ChangePassword(UpdatePasswordDto updatePasswordDto)
        {
            updatePasswordDto.UserName = User.Identity.Name;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updatePasswordDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var respMessage = await client.PostAsync("https://localhost:7175/api/User/ChangePassword", content);
            if (respMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminList");
            }
            return View();
        }

    }
}
