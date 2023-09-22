using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents.Contact
{
	public class MailContact : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
