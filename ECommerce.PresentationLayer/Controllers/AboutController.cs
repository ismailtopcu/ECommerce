using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
