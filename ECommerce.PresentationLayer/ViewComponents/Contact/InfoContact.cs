using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents.Contact
{
	public class InfoContact : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
