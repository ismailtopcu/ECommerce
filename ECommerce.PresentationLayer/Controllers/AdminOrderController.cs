using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Models;
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

            List<OrderWithUserViewModel> list = new List<OrderWithUserViewModel>();

            foreach (var item in values) 
            {
                OrderWithUserViewModel order = new()
                {
                    Id = item.Id,
                    OrderDate = item.OrderDate,
                    UserId = item.UserId,
                    TotalAmount = item.TotalAmount,
                    User = await _apiService.GetData<AppUser>("https://localhost:7175/api/User/GetOneUserById/"+ item.UserId)
                };
                list.Add(order);
            }


            return View(list.ToPagedList(page, 10));
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
