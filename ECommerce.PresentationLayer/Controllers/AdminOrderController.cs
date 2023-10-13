using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace ECommerce.PresentationLayer.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly IApiService _apiService;

        public AdminOrderController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [Route("/adminpanel/orders")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var values = await _apiService.GetTableData<ResultOrderDto>("https://localhost:7175/api/Order/");
            return View(values.ToPagedList(page, 10));
        }

        [Route("/adminpanel/orders/orderdetail/{id}")]
		public async Task<IActionResult> OrderDetail(int id)
		{
			var values = await _apiService.GetTableData<ResultOrderDto>("https://localhost:7175/api/Order/");
			var order = values.Where(x => x.Id == id).FirstOrDefault();

            var user = await _apiService.GetData<ResultUserDto>("https://localhost:7175/api/User/GetOneUserById/"+order.UserId );
            ViewBag.User = user.Name + " " + user.Surname;
            ViewBag.Email = user.Email;
			return View(order);
		}
	}
}
