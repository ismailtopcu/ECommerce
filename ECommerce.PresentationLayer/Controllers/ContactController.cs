using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
	[AllowAnonymous]
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
