using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminLayoutController : Controller
    {
        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult PreloaderPartial() 
        {
            return PartialView();
        }
        public PartialViewResult NavHeaderPartial() 
        {
            return PartialView();
        }
        public PartialViewResult SidebarPartial() 
        {
            return PartialView();
        }
        public PartialViewResult FooterPartial() 
        {
            return PartialView();
        }
        public PartialViewResult ScriptsPartial() 
        {
            return PartialView();
        }
    }
}
