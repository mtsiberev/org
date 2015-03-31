using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Organizations;

namespace OrganizationsWebApplication.Controllers
{
    public class FacadeController : Controller
    {
        //
        // GET: /Facade/
        public ActionResult Index()
        {
            var container = new RegisterByContainer().Container;
            var facade = container.GetInstance<Facade>();
   
            return View(facade);
        }

    }
}
