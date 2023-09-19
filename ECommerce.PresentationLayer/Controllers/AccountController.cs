using AutoMapper;
using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IMapper _mapper;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginUserDto loginUserDto)
		{
			var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, false, false);
			if (result.Succeeded)
			{
				return RedirectToAction("Index","Home");
			}
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
				return View();
			}
			var appUser = _mapper.Map<AppUser>(createNewUserDto);
			var result = await _userManager.CreateAsync(appUser,createNewUserDto.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Login");
			}
			return View();
		}
	}
}
