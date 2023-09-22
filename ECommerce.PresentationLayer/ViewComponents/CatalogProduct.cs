using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents
{
	public class CatalogProduct:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
