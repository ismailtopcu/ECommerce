using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.DtoLayer.Dtos.BasketDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using ECommerce.PresentationLayer.Services;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.DtoLayer.Dtos.OrderDetail;
using ECommerce.DtoLayer.Dtos.Product;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Humanizer;
using AutoMapper;

namespace ECommerce.PresentationLayer.Controllers
{
	public class CartController : Controller
	{
		private readonly IBasketService _basketService;
		private readonly ApiService _apiService;
		private readonly IMapper _mapper;

        public CartController(IBasketService basketService, ApiService apiService, IMapper mapper)
        {
            _basketService = basketService;
            _apiService = apiService;
            _mapper = mapper;
        }

        [HttpGet]
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
        public async Task<IActionResult> EndOrder()
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var basketDto = HttpContext.Session.Get<BasketDto>("basket");
			if (basketDto == null)
			{
				ModelState.AddModelError("", "Sepetiniz boş");
				return RedirectToAction("Index");
			}
			if (basketDto.TotalPrice == 0 || basketDto.BasketItems.Sum(x => x.Product.Price) == 0)
			{
                ModelState.AddModelError("", "Sepetiniz boş");
                return RedirectToAction("Index");
			}
            CreateOrderDto orderDto = new();
            orderDto.UserId = Convert.ToInt32(User.Identity.GetUserId());

            string urlCreateOrder = "https://localhost:7175/api/Order/CreateOrder";
            await _apiService.AddData(urlCreateOrder, orderDto);

			string urlGetAllOrders = "https://localhost:7175/api/Order/";
            var orders = await _apiService.GetTableData<ResultOrderDto>(urlGetAllOrders);
			var createdOrder = orders.Where(x => x.UserId == orderDto.UserId & x.OrderFinished == false).FirstOrDefault();
            int idOrder = createdOrder.Id;

            foreach (var item in basketDto.BasketItems)
            {
                CreateOrderDetailDto dto = new(idOrder, item.Product.Id, item.Quantity, item.Product.Price);
                string urlForOrderDetail = "https://localhost:7175/api/OrderDetail/";
                await _apiService.AddData(urlForOrderDetail, dto);
            }

            string urlForOrderFinish = "https://localhost:7175/api/Order/FinishOrder?id="+createdOrder.Id;
            var result = await _apiService.GetNoContent(urlForOrderFinish);
			if (result == true)
			{
				return RedirectToAction("Index","Home");
			}
            ModelState.AddModelError("", "Sistemde bir hata meydana geldi.");
            return RedirectToAction("Index");

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
