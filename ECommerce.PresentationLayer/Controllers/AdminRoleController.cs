using Microsoft.AspNetCore.Mvc;

namespace ECommerce.PresentationLayer.Controllers
{
    public class AdminRoleController : Controller
    {
        public IActionResult RoleList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddRole() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRole(string roleName)
        {
            return RedirectToAction("RoleList", "AdminRole");
        }
        public IActionResult UserRoleEdit() 
        {
            return View();
        }
        //public IActionResult UserRoleEdit()
        //{
        //    return RedirectToAction("RoleList","AdminRole");
        //}
        public IActionResult UserRoleDelete() 
        {
            return View();
        }
    }
}
