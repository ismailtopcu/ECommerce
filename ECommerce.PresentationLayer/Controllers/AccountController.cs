using AutoMapper;
using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Messages;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Services;
using ECommerce.PresentationLayer.Services.RabbitMQEvents;
using ECommerce.PresentationLayer.Services.RabbitMQServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IApiService _apiService;
        private readonly RabbitMqPublisher _rabbitMqPublisher;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IApiService apiService, RabbitMqPublisher rabbitMqPublisher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _apiService = apiService;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, loginUserDto.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            if (result.IsNotAllowed)
            {
                var user = await _userManager.FindByNameAsync(loginUserDto.Username);
				TempData["email"] = user.Email;
				return RedirectToAction("VerifyEmail", new { email = user.Email });
            }
            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateNewUserDto createNewUserDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Bilgilerinizi lütfen kontrol ediniz");
                return View();
            }

            var isEmailUsed = await _userManager.FindByEmailAsync(createNewUserDto.Mail);
            if(isEmailUsed != null)
            {
                return View();
            }

            string url = "https://localhost:7175/api/User/CreateUser";
            var result = await _apiService.AddData(url, createNewUserDto);
            if (result == true) 
            {
                TempData["email"] = createNewUserDto.Mail;
				return RedirectToAction("VerifyEmail", new { email = createNewUserDto.Mail }); 
            }

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string? email)
        {
            //Email varlığı kontrol edilip aktarılmadıysa TempDatadan çekiliyor.
            if(email == null) { email = TempData["email"].ToString(); }
            var user = await _userManager.FindByEmailAsync(email);
			if (user == null) { return View(); }

			//ConfirmCode ataması yapılıyor.
			Random random = new Random();
			int code;
			code = random.Next(100000, 1000000);
            user.ConfirmCode = code;
            await _userManager.UpdateAsync(user);

            //Confirm Code kullanıcının email adresine yollanıyor.
			string url = "https://localhost:7175/api/AdminMessage/SendEmail";
            CreateMailDto mailDto = new ();
            mailDto.Email = email;
            mailDto.To = email;
            mailDto.Subject = "E-Posta Adresi Doğrulama Kodu";
            mailDto.Body = $"ModaMania'ya hoşgeldiniz! Bize katılmanız için son bir adımınız kaldı. E-Posta adresi doğrulama kodunuz: {user.ConfirmCode}";
            await _apiService.AddData(url, mailDto);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDto verifyEmailDto)
        {
            string url = "https://localhost:7175/api/User/VerifyEmail";
            verifyEmailDto.Email = TempData["email"].ToString();
            var result = await _apiService.AddData(url, verifyEmailDto);

            if (result == true) 
            {
                var user = await _userManager.FindByEmailAsync(verifyEmailDto.Email);
                _rabbitMqPublisher.Publish(new WelcomeMailCreatedEvent() { Email = user.Email, NameSurname = user.Name +" "+ user.Surname, UserName = user.UserName });
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Doğrulama kodu yanlış");
            return View();
        }





        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var passwordLink = Url.Action("ResetPassword", "Account", new
                {
                    userName = user.UserName,
                    token = passwordResetToken
                }, HttpContext.Request.Scheme);

                //Confirm Code kullanıcının email adresine yollanıyor.
                string url = "https://localhost:7175/api/AdminMessage/SendEmail";
                CreateMailDto mailDto = new();
                mailDto.Email = email;
                mailDto.To = email;
                mailDto.Subject = "Şifre Sıfırlama";
                mailDto.Body = $"Lütfen şifrenizi sıfırlamak için <a href='{passwordLink}'>linke</a> tıklayınız.";
                await _apiService.AddData(url, mailDto);
                return RedirectToAction("ForgotPasswordWaitRoom");
            }
            ModelState.AddModelError("", "Bu e-posta adresine kayıtlı bir kullanıcı bulunmamaktadır");
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPasswordWaitRoom()
        {
            return View();
        }




        [HttpGet]
        public IActionResult ResetPassword(string userName, string token)
        {
            ResetPasswordDto passwordDto = new ();
            passwordDto.Token = token;
            passwordDto.UserName = userName;
			return View(passwordDto);
        }
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
		{
            var user = await _userManager.FindByNameAsync(resetPasswordDto.UserName);
            if(user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    return RedirectToAction("Login");
                }
                return RedirectToAction("View");
            }

            return View();
		}


        public async Task<IActionResult> LogoutSec()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
