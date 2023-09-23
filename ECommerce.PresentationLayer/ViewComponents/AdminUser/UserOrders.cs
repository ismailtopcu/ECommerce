using ECommerce.DtoLayer.Dtos.AccountDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ECommerce.PresentationLayer.ViewComponents.AdminUser
{
    public class UserOrders 
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public UserOrders(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}

        //public async Task<IViewComponentResult> Invoke(string userName)
        //{

        //    var client = _httpClientFactory.CreateClient();
        //    var responseMessage = await client.GetAsync("https://localhost:7175/api/User/GetOneUser/" + userName);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        //        var values = JsonConvert.DeserializeObject<ResultUserDetailDto>(jsonData);
        //        return View(values);
        //    }
        //    return View();
        //}
    }
}
