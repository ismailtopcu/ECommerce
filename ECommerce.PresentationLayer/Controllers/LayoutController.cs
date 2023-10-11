using ECommerce.DtoLayer.Dtos.BasketDto;
using ECommerce.PresentationLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
	[AllowAnonymous]
	public class LayoutController : Controller
	{
		public PartialViewResult FooterPartial()
		{
			return PartialView();
		}
		public PartialViewResult HeadPartial()
		{
			return PartialView();
		}
		public PartialViewResult HeaderPartial()
		{
			var basketDto = HttpContext.Session.Get<BasketDto>("basket");

			int basketItemCount = basketDto?.BasketItems?.Count ?? 0;

			ViewData["BasketItemCount"] = basketItemCount;

			return PartialView();
		}
		public PartialViewResult CartPartial()
		{
			return PartialView();
		}
		public PartialViewResult ScriptPartial()
		{
			return PartialView();
		}
	}
}
