using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Employee;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ECommerce.PresentationLayer.Controllers
{
    public class AdminEmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminEmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("adminpanel/employeelist")]
        public async Task<IActionResult> EmployeeList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Employee/GetAllEmployees");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEmployeeDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("adminpanel/updateemployee/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Employee/GetEmployeeById/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateEmployeeDto>(jsonData);
                return View(values);
            }
            return RedirectToAction("EmployeeList");
        }
        [Route("adminpanel/updateemployee/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateEmployeeDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7175/api/Employee/UpdateEmployee", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList");
            }
            return View();
        }

        [Route("adminpanel/deleteemployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7175/api/Employee/DeleteEmployee/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList");
            }
            return View();
        }

        [Route("adminpanel/addemployee")]
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }
        [Route("adminpanel/addemployee")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createEmployeeDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var respMessage = await client.PostAsync("https://localhost:7175/api/Employee/CreateEmployee", content);
            if (respMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList");
            }
            return View();
        }
    }
}
