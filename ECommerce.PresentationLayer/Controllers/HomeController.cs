using ECommerce.BusinessLayer.Concrete;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Category;
using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace ECommerce.PresentationLayer.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, Context context)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        public async Task< IActionResult> Index()
		{
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/Product/GetAllProducts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
       public IActionResult Profile()
		{
            return View();
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}