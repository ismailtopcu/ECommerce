using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DtoLayer.Dtos.AccountDto;
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
        private readonly IServiceProvider _serviceProvider;
    public ProfileController(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
    {
        _httpClientFactory = httpClientFactory;
        _serviceProvider = serviceProvider;
    }
    public async Task<IActionResult> UserProfile()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7175/api/User/GetUserById/" + User.Identity.GetUserId() ); // Update the API endpoint accordingly
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
        var responseMessage = await client.GetAsync("https://localhost:7175/api/User/GetUserById/" + User.Identity.GetUserId() );

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
        using var dbContext = _serviceProvider.GetRequiredService<Context>();
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserName);

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

                user.Name = userDto.Name;
                user.Surname = userDto.Surname;
                user.PhoneNumber = userDto.PhoneNumber;
                user.City = userDto.City;
                user.ImageUrl = imageUrl;
                dbContext.SaveChanges();

                return RedirectToAction("UserProfile");
            }
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.PhoneNumber = userDto.PhoneNumber;
            user.City = userDto.City;
            dbContext.SaveChanges();
            return RedirectToAction("UserProfile");
        }

        return View(userDto);
    }


}

