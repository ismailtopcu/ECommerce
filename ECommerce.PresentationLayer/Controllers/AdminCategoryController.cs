using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
    public class AdminCategoryController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }
    }
}
