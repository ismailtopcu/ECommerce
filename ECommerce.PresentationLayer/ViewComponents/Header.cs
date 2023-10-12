using ECommerce.DtoLayer.Dtos.BasketDto;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.ViewComponents
{
	public class Header:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var basketDto = HttpContext.Session.Get<BasketDto>("basket");

			int basketItemCount = basketDto?.BasketItems?.Count ?? 0;

			ViewBag.BasketItemCount = basketItemCount;
			return View();
		}
	}
}
