using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.AdminPanel;
using ECommerce.DtoLayer.Dtos.Category;
using ECommerce.DtoLayer.Dtos.Employee;
using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminPanelController : Controller
	{
		private readonly IApiService _apiService;

        public AdminPanelController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
		{
			var categoryCount = await _apiService.GetTableData<ResultCategoryDto>("https://localhost:7175/api/Category/GetAllCategories");
			var productCount = await _apiService.GetTableData<ResultProductDto>("https://localhost:7175/api/Product/GetAllProducts");
			var employeeCount = await _apiService.GetTableData<ResultEmployeeDto>("https://localhost:7175/api/Employee/GetAllEmployees");
			var userCount = await _apiService.GetTableData<ResultUserDto>("https://localhost:7175/api/User/GetAllMembers");
			//var orderCount = await _apiService.GetTableData<ResultCategoryDto>("https://localhost:7175/api/Category/GetAllCategories");

			var adminSum = new AdminSummaryDTO
			{
				CategoryCount=categoryCount.Count,
				ProductCount = productCount.Count,
				EmployeeCount =employeeCount.Count,
				UserCount=userCount.Count,
				//OrderCount=orderCount.Count
			};

			return View(adminSum);

		}
	}
}
