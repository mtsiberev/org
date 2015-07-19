using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.MvcHelpers;

namespace OrganizationsWebApplication.Controllers
{
    public class AdministrationMenuController : Controller
    {
        //
        // GET: /AdministrationMenu/
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
