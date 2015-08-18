using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

    }
}
