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
            var organizationRepository = container.GetInstance<IRepository<Organization>>();
            var departmentRepository = container.GetInstance<IRepository<Department>>();
            var employeeRepository = container.GetInstance<IRepository<Employee>>();
            var facade = new Facade(organizationRepository, departmentRepository, employeeRepository);
            facade.Init();

            return View(facade.GetRandomEmployee().Age);
        }

    }
}
