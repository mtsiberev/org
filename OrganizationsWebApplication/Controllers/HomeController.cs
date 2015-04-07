using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private Facade m_facade = RegisterByContainer.Instance.Container.GetInstance<Facade>();
        
        public ActionResult Index()
        {
            var organizations =
                from organization in m_facade.GetAllOrganizations()
                where (true)
                select new SimpleOrganization() {Name = organization.Name, Id = organization.Id};
            return View(new OrganizationModels(organizations.ToList() ) );
        }
        
        public ActionResult Department(int i)
        {
            var dep = m_facade.GetDepartmentById(i);
            return View(dep);
        }

    }
}
