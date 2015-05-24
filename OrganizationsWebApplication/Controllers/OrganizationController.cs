using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    public class OrganizationController : Controller
    {
        //
        // GET: /Organization/
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult OrganizationInfo(int id = 0)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var departments =
                from department in m_facade.GetAllDepartments()
                where department.ParentOrganization.Id == id
                select new DtoDepartment() { Name = department.Name, Id = department.Id };
            return View(new OrganizationModel() { Id = id, Departments = departments.ToList(), Name = name });
        }

        public ActionResult AddOrganizationMenu()
        {
            return View();
        }

        public ActionResult AddOrganization(string name)
        {
            m_facade.AddOrganization(new Organization(0) { Name = name });
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdateOrganizationMenu(int id)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organization = new OrganizationModel() { Id = id, Departments = null, Name = name };
            return View(organization);
        }

        public ActionResult UpdateOrganization(OrganizationModel organization)
        {
            m_facade.UpdateOrganization(new Organization(organization.Id) { Name = organization.Name });
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteOrganization(int id = 0)
        {
            m_facade.DeleteOrganization(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
