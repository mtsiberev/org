using System.Web.Mvc;
using System.Web.Routing;

namespace OrganizationsWebApplication.Controllers
{
    public class ChangeCultureController : Controller
    {
        //
        // GET: /ChangeCulture/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeCulture(string culture, string url)
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeCultureRoute(string culture, RouteData data)
        {
            return RedirectToAction("Index", "Home");
        }



    }
}
