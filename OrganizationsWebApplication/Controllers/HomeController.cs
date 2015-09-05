using System.Web.Mvc;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("OrganizationsList", "OrganizationsList");
        }
    }
}
