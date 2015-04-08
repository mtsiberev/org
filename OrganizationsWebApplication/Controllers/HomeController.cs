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

            return View(new OrganizationListModels(organizations.ToList() ) );
        }
        
        public ActionResult OrganizationInfo(SimpleOrganization organization = null)
        {
            var departments =
                from department in m_facade.GetAllDepartments()
                where department.ParentOrganization.Id == organization.Id
                select new SimpleDepartment() {Name = department.Name, Id = department.Id};

            return View(new OrganizationModel(departments.ToList(), organization.Name));
        }

    }
}
