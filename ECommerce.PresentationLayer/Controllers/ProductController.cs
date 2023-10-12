using ECommerce.DtoLayer.Dtos.Category;
using ECommerce.DtoLayer.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace ECommerce.PresentationLayer.Controllers
{
	[AllowAnonymous]
	public class ProductController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index(string searchTerm)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7175/api/Product/GetAllProducts?searchTerm="+searchTerm);
			if(responseMessage.IsSuccessStatusCode)
			{
				var jsonData=await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
				return View(values);
			}
			return View();
		}
		public async Task< IActionResult> ProductDetail(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7175/api/Product/GetProductById/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<ResultProductDto>(jsonData);
				return View(values);
			}
			return View();
		}        
        
    }
}
