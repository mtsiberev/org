using System.Web.Mvc;
using Organizations;
using Organizations.Entity;
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
            var user = new User(1,"Petrov", "admin");
            return View(user);
        }
        
        [HttpPost]
        public ActionResult ChangeRolesMenu(User user)
        {
            return View(user);
        }



    }
}
