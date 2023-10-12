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

        //Üye bilgilerini çeker
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMembersAsync()
        {
            var values = await _userManager.GetUsersInRoleAsync("Member");
            return Ok(values);
        }

        //Kullanıcı siler
        [HttpDelete("[action]/{userName}")]
        public async Task<IActionResult> DeleteUserAsync(string userName)
        {
            var value = await _userManager.FindByNameAsync(userName);
            if (value == null) { return BadRequest("Kullanıcı bulunamadı."); }

            await _userManager.DeleteAsync(value);
            return Ok("Başarıyla silindi");
        }

        //Sadece bir üye bilgilerini getirir.
        [HttpGet("[action]/{userName}")]
        public async Task<IActionResult> GetOneUserAsync(string userName)
        {
            var value = await _userManager.FindByNameAsync(userName);
            if (value == null) { return BadRequest("Kullanıcı bulunamadı."); }

            var isAdmin = await _userManager.IsInRoleAsync(value, "Admin");
            if (isAdmin == true) { return BadRequest("Admin bilgisi çekilemez."); }
            return Ok(value);
        }


        //Kullanıcı günceller
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDto updateUserDto)
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

        //Üye oluşturur.
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUserAsync(CreateNewUserDto createNewUserDto)
        {

            var user = new AppUser
            {
                UserName = createNewUserDto.Username,
                Email = createNewUserDto.Mail,
                Name = createNewUserDto.Name,
                Surname = createNewUserDto.Surname,
                City = createNewUserDto.City
            };

            var result = await _userManager.CreateAsync(user, createNewUserDto.Password);
            if (!result.Succeeded) { return BadRequest("Bir hata meydana geldi."); }

            await _userManager.AddToRoleAsync(user, "Member");

            return Ok("Kullanıcı oluşturuldu");

        }

        //Kullanıcı şifresi değiştirir.
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePasswordAsync(UpdatePasswordDto updatePasswordDto)
        {
            var user = await _userManager.FindByNameAsync(updatePasswordDto.UserName);
            if (user == null) { return BadRequest("Kullanıcı bulunamadı"); }

            var result = await _userManager.ChangePasswordAsync(user, updatePasswordDto.OldPassword, updatePasswordDto.Password);
            if (!result.Succeeded) { return BadRequest(); }
            return Ok("Şifre güncellendi");
        }

        //Kullanıcıya rol verir.
        [HttpPost("[action]/{userName},{role}")]
        public async Task<IActionResult> AddRoleAsync(string userName, string role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded) { return BadRequest("Bir hata meydana geldi"); }
            return Ok("Kullanıcı role eklendi");
        }

        //Epostaya göre eposta doğrular
        [HttpPost("[action]")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDto verifyEmailDto)
        {
            var user = await _userManager.FindByEmailAsync(verifyEmailDto.Email);
            if (user == null) { return BadRequest(); }

            if (verifyEmailDto.Code == user.ConfirmCode.ToString())
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                return Ok();
            }
            return BadRequest();

        }
        //Epostaya göre eposta doğrular
        [HttpPost("[action]")]
        public async Task<IActionResult> VerifyEmailByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) { return BadRequest(); }
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            return Ok();
        }





        //Şifre resetler.
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByNameAsync(resetPasswordDto.UserName);
            if (user != null)
            {

                var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    return Ok();
                }
                return Ok(result.Errors.ToList());
            }
            return NotFound();
        }

    }
}
