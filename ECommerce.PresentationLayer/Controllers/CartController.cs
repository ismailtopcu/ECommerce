using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.DtoLayer.Dtos.BasketDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using ECommerce.PresentationLayer.Services;

namespace ECommerce.PresentationLayer.Controllers
{
	public class CartController : Controller
	{
		private readonly IBasketService _basketService;

		public CartController(IBasketService basketService)
		{
			_basketService = basketService;
		}


		public IActionResult Index()
		{
			var basketDto = HttpContext.Session.Get<BasketDto>("basket");
			if (basketDto == null)
			{
				basketDto = new BasketDto();				
			}

			return View(basketDto);
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(int productId, int quantity)
		{
			var basketDto = HttpContext.Session.Get<BasketDto>("basket");
			if (basketDto == null)
			{
				basketDto = new BasketDto();				
			}

			var basket = await _basketService.AddToBasket(basketDto, productId, quantity);

			HttpContext.Session.Set<BasketDto>("basket", basket);

			return Json(basket);
		}
		
		public  IActionResult RemoveToCart(int productId)
		{
			var basketDto = HttpContext.Session.Get<BasketDto>("basket");

			var basket =  _basketService.RemoveToBasket(basketDto, productId);

			HttpContext.Session.Set<BasketDto>("basket", basket);

			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult GetCartData()
		{
			var basketDto = HttpContext.Session.Get<BasketDto>("basket");
			if (basketDto == null)
			{
				basketDto = new BasketDto();
			}

			return Json(basketDto); // Sepet verilerini JSON formatında döndürün
		}
	}
}
