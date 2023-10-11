using ECommerce.DtoLayer.Dtos.BasketDto;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents
{
	public class CartComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var basketDto = HttpContext.Session.Get<BasketDto>("basket");
			if (basketDto == null)
			{
				basketDto = new BasketDto();
			}

			return View(basketDto);
		}
	}
}
