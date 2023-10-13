using ECommerce.DtoLayer.Dtos.Category;
using ECommerce.DtoLayer.Dtos.Comment;
using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNet.Identity;
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
		private readonly IApiService _apiService;

        public ProductController(IHttpClientFactory httpClientFactory, IApiService apiService)
        {
            _httpClientFactory = httpClientFactory;
            _apiService = apiService;
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

		[HttpPost]
		public async Task<IActionResult> AddComment(string review, string productId)
		{
			CreateCommentDto dto = new()
			{
				CommentText = review,
				CreatedDate = DateTime.Now,
				ProductId = Convert.ToInt32(productId),
				UserId = Convert.ToInt32(User.Identity.GetUserId())
			};
			await _apiService.AddData("https://localhost:7175/api/Comment", dto);
			return RedirectToAction("ProductDetail", new {id = productId});
		}
        
    }
}
