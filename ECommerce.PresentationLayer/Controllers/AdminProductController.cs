using ECommerce.DtoLayer.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using X.PagedList;

namespace ECommerce.PresentationLayer.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminProductController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public AdminProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[Route("adminpanel/productlist")]
		public async Task<IActionResult> ProductList(int page = 1)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7175/api/Product/GetAllProducts");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
				return View(values.ToPagedList(page,10));
			}
			return View();
		}

		[Route("adminpanel/searchedproducts/{searchedKey}")]
		[HttpGet]
		public async Task<IActionResult> SearchedProductList(string searchedKey, int page = 1)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7175/api/Product/GetSearchedProducts/"+searchedKey);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
				return View(values.ToPagedList(page, 10));
			}
			return View();
		}

		[Route("adminpanel/updateproduct/{id}")]
		[HttpGet]
		public async Task<IActionResult> UpdateProduct(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7175/api/Product/GetProductById/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
				return View(values);
			}
			return RedirectToAction("ProductList");
		}
		[Route("adminpanel/updateproduct/{id}")]
		[HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:7175/api/Product/UpdateProduct", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("ProductList");
			}
			return View();
		}

		public async Task<IActionResult> DeleteProduct(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync("https://localhost:7175/api/Product/DeleteProduct?id=" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("ProductList");
			}
			return View();
		}

		[Route("adminpanel/addproduct")]
		[HttpGet]
		public IActionResult CreateProduct()
		{
			return View();
		}
		[Route("adminpanel/addproduct")]
		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createProductDto);
			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var respMessage = await client.PostAsync("https://localhost:7175/api/Product/AddProduct", content);
			if (respMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("ProductList");
			}
			return View();
		}
	}
}
