using System;
using System.Linq;
using System.Web.Mvc;
using Organizations;
using Organizations.Helpers;
using Organizations.Mappers;
using OrganizationsWebApplication.Models;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                Response.Redirect("~/account/login");
            }
            return RedirectToAction("OrganizationsList", "OrganizationsList");
        }
    }
}
