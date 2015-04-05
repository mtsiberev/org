using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Organizations;
using StructureMap;

namespace OrganizationsWebApplication.Controllers
{
    public class FacadeController : Controller
    {
        private Facade facade;
        public FacadeController()
        {
            var container = RegisterByContainer.Instance.Container;
            facade = container.GetInstance<Facade>();
            facade.Init();
        }

        //
        // GET: /Facade/
        public ActionResult Index()
        {
            return View(facade);
        }

        public ActionResult Department(int i)
        {
            var dep = facade.GetDepartmentById(i);
            return View(dep);
        }

    }
}
