using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.AdminPanel;
using ECommerce.DtoLayer.Dtos.Category;
using ECommerce.DtoLayer.Dtos.Employee;
using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents.AdminLayout
{
    public class InfoLabels : ViewComponent
    {
        private readonly ApiService _apiService;

        public InfoLabels(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryCount = await _apiService.GetTableData<ResultCategoryDto>("https://localhost:7175/api/Category/GetAllCategories");
            var productCount = await _apiService.GetTableData<ResultProductDto>("https://localhost:7175/api/Product/GetAllProducts");
            var employeeCount = await _apiService.GetTableData<ResultEmployeeDto>("https://localhost:7175/api/Employee/GetAllEmployees");
            var userCount = await _apiService.GetTableData<ResultUserDto>("https://localhost:7175/api/User/GetAllMembers");
            //var orderCount = await _apiService.GetTableData<ResultCategoryDto>("https://localhost:7175/api/Category/GetAllCategories");

            var adminSum = new AdminSummaryDTO
            {
                CategoryCount = categoryCount.Count,
                ProductCount = productCount.Count,
                EmployeeCount = employeeCount.Count,
                UserCount = userCount.Count,
                //OrderCount=orderCount.Count
            };

            return View(adminSum);

        }
    }
}
