using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Roles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.PresentationLayer.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminRoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("adminpanel/rolelist")]
        public async Task<IActionResult> RoleList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Role");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultRoleDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        [Route("adminpanel/addrole")]
        public IActionResult AddRole() 
        {
            return View();
        }
        [HttpPost]
        [Route("adminpanel/addrole")]
        public async Task<IActionResult> AddRole(CreateRoleDto createRoleDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createRoleDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var respMessage = await client.PostAsync("https://localhost:7175/api/Role", content);
            if (respMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("RoleList", "AdminRole");
            }
            return View();
        }

        [HttpGet]
        [Route("adminpanel/editrole/{id}")]
        public async Task<IActionResult> RoleEdit(int id) 
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Role/"+ id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateRoleDto>(jsonData);
                return View(values);
            }
            return RedirectToAction("RoleList", "AdminRole");
        }
        [HttpPost]
        [Route("adminpanel/editrole/{id}")]
        public async Task<IActionResult> RoleEdit(UpdateRoleDto updateRoleDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateRoleDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7175/api/Role", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("RoleList", "AdminRole");
            }
            return View();
        }

        [Route("adminpanel/deleterole/{id}")]
        public async Task<IActionResult> DeleteRole(int id) 
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7175/api/Role?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("RoleList");
            }
            return RedirectToAction("RoleList");
        }
    }
}
