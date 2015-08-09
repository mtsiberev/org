using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class AdministrationMenuController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult AddNewUserMenu()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangeRolesMenu()
        {
            var user = new User("Petrov", "admin");
            return View(user);
        }
        
        [HttpPost]
        public ActionResult ChangeRolesMenu(User us)
        {
            return View(us);
        }



    }
}
