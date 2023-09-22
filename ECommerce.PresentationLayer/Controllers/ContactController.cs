using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
