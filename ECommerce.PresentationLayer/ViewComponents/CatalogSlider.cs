using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents
{
	public class CatalogSlider:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
