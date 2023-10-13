using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents.AdminOrder
{
    public class _OrderDetailUser : ViewComponent
    {
        private readonly IApiService _apiService;

        public _OrderDetailUser(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName) 
        {
            var user = await _apiService.GetData<ResultUserDto>("https://localhost:7175/api/User/GetOneUser/"+ userName);

            var values = await _apiService.GetTableData<ResultOrderDto>("https://localhost:7175/api/Order/");
            var userOrders = values.Where(x => x.UserId == user.Id).ToList();
            return View(userOrders);
        }
    }
}
