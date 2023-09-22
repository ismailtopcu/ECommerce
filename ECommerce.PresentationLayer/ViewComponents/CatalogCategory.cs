using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents
{
	public class CatalogCategory : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
