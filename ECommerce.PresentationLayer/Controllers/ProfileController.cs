using AutoMapper;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Models;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.PresentationLayer.Controllers;

public class ProfileController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IApiService _apiService;
    private readonly IMapper _mapper;
    public ProfileController(IHttpClientFactory httpClientFactory, IApiService apiService, IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _apiService = apiService;
        _mapper = mapper;
    }
    public async Task<IActionResult> UserProfile()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/User/GetOneUserById/" + User.Identity.GetUserId()); 
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var userDto = JsonConvert.DeserializeObject<ResultUserDetailDto>(jsonData);
                return View(userDto);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:7175/api/User/GetOneUserById/" + User.Identity.GetUserId());
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<UpdateUserDto>(jsonData);
            return View(userDto);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UpdateUserDto userDto, IFormFile ImageFile)
    {
        var user = await _apiService.GetData<AppUser>("https://localhost:7175/api/User/GetOneUser/" + User.Identity.Name); 
        if (user != null)
        {
            var oldImagePath = user.ImageUrl;
            
            if (ImageFile != null)
            {
                var randomImageName = userDto.UserName + Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", randomImageName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                var imageUrl = "/images/" + randomImageName;
                userDto.ImageUrl = imageUrl;

                if (!string.IsNullOrEmpty(oldImagePath))
                {
                    var oldImagePathOnServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePathOnServer))
                    {
                        System.IO.File.Delete(oldImagePathOnServer);
                    }
                }

                var client1 = _httpClientFactory.CreateClient();
                var jsonData1 = JsonConvert.SerializeObject(userDto);
                StringContent stringContent1 = new StringContent(jsonData1, Encoding.UTF8, "application/json");
                var responseMessage1 = await client1.PutAsync("https://localhost:7175/api/Admin/UpdateAdmin", stringContent1);
                if (responseMessage1.IsSuccessStatusCode) 
                {
                    return RedirectToAction("UserProfile");
                }
                return View();
            }
            userDto.ImageUrl = oldImagePath;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(userDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7175/api/Admin/UpdateAdmin", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("UserProfile");
            }
            return View();
        }

        return View(userDto);
    }

    public async Task<IActionResult> UserOrderDetails(int id)
    {
        var values = await _apiService.GetTableData<ResultOrderDto>("https://localhost:7175/api/Order/");
        var order = values.Where(x => x.Id == id).FirstOrDefault();

        var user = await _apiService.GetData<ResultUserDto>("https://localhost:7175/api/User/GetOneUserById/" + order.UserId);
        ViewBag.User = user.Name + " " + user.Surname;
        ViewBag.Email = user.Email;
        return View(order);
    }

}

