using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.PresentationLayer.ViewComponents.AdminUser
{
    public class UserProfile : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiService _apiService;
        public UserProfile(IHttpClientFactory httpClientFactory, ApiService apiService)
        {
            _httpClientFactory = httpClientFactory;
            _apiService = apiService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/User/GetOneUser/" + userName);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultUserDetailDto>(jsonData);

                var valuesOrders = await _apiService.GetTableData<ResultOrderDto>("https://localhost:7175/api/Order/");
                var userOrders = valuesOrders.Where(x => x.UserId == values.Id).ToList();
                ViewBag.count = userOrders.Count();

                return View(values);
            }
            return View();
        }
    }
}
