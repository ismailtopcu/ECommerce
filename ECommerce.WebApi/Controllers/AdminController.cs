using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        //Admin bilgilerini çeker.
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAdminsAsync()
        {
            var values = await _userManager.GetUsersInRoleAsync("Admin");

            return Ok(values);
        }

        //Kullanıcı siler
        [HttpDelete("[action]/{userName}")]
        public async Task<IActionResult> DeleteAdminAsync(string userName)
        {
            var value = await _userManager.FindByNameAsync(userName);
            if (value == null) { return BadRequest("Kullanıcı bulunamadı."); }

            await _userManager.DeleteAsync(value);
            return Ok("Başarıyla silindi");
        }

        //Sadece bir admin bilgilerini getirir.
        [HttpGet("[action]/{userName}")]
        public async Task<IActionResult> GetOneAdminAsync(string userName)
        {
            var value = await _userManager.FindByNameAsync(userName);
            if (value == null) { return BadRequest("Kullanıcı bulunamadı."); }

            var isMember = await _userManager.IsInRoleAsync(value, "Member");
            if (isMember == true) { return BadRequest("Üye bilgisi çekilemez."); }
            return Ok(value);
        }

        //Admin oluşturur.
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAdminAsync(CreateNewUserDto createNewUserDto)
        {
            var user = new AppUser
            {
                UserName = createNewUserDto.Username,
                Email = createNewUserDto.Mail,
                Name = createNewUserDto.Name,
                Surname = createNewUserDto.Surname,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, createNewUserDto.Password);
            if (!result.Succeeded) { return BadRequest("Bir hata meydana geldi."); }

            await _userManager.AddToRoleAsync(user, "Admin");

            return Ok("Admin oluşturuldu");

        }

        //Kullanıcı günceller
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAdminAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByNameAsync(updateUserDto.UserName);

            user.City = updateUserDto.City;
            user.Surname = updateUserDto.Surname;
            user.Name = updateUserDto.Name;
            user.ImageUrl = updateUserDto.ImageUrl;
            user.PhoneNumber = updateUserDto.PhoneNumber;

            await _userManager.UpdateAsync(user);
            return Ok("Başarıyla güncellendi");
        }
    }
}
