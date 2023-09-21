using AutoMapper;
using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Category;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsersAsync() 
        {
            var values = await _userManager.Users.ToListAsync();
            return Ok(values);
        }

        [HttpDelete("[action]/{userName}")]
        public async Task<IActionResult> DeleteUserAsync(string userName) 
        {
            var value = await _userManager.FindByNameAsync(userName);
            if (value == null) { return BadRequest("Kullanıcı bulunamadı."); }

            await _userManager.DeleteAsync(value);
            return Ok("Başarıyla silindi");
        }

        [HttpGet("[action]/{userName}")]
        public async Task<IActionResult> GetOneUserAsync(string userName) 
        {
            var value = await _userManager.FindByNameAsync(userName);
            if (value == null) { return BadRequest("Kullanıcı bulunamadı."); }

            return Ok(value);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByNameAsync(updateUserDto.UserName);

            user.City = updateUserDto.City;
            user.Surname = updateUserDto.Surname;
            user.Name = updateUserDto.Name;
            user.ImageUrl = updateUserDto.ImageUrl;

            await _userManager.UpdateAsync(user);
            return Ok("Başarıyla güncellendi");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUserAsync(CreateNewUserDto createNewUserDto)
        {
            var user = new AppUser
            {
                UserName = createNewUserDto.Username,
                Email = createNewUserDto.Mail,
                Name = createNewUserDto.Name,
                Surname = createNewUserDto.Surname,
                City = " "
            };
            var result = await _userManager.CreateAsync(user, createNewUserDto.Password);
            if (!result.Succeeded) { return BadRequest("Bir hata meydana geldi."); }

            return Ok("Kullanıcı oluşturuldu");

        }

        [HttpPost("[action]/{userName},{currentPassword},{newPassword}")]
        public async Task<IActionResult> ChangePasswordAsync(string userName, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded) { return BadRequest(); }
            return Ok("Şifre güncellendi");
        }

        [HttpPost("[action]/{userName},{role}")]
        public async Task<IActionResult> AddRoleAsync(string userName,string role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.AddToRoleAsync(user,role);
            if (!result.Succeeded) { return BadRequest("Bir hata meydana geldi"); }
            return Ok("Kullanıcı role eklendi");

        }
    }
}
