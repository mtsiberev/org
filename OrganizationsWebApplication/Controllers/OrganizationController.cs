using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.MvcHelpers;

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
            var page = Paginator.GetPageObject(m_facade.GetOrganizationsCount());
            var currentPageNumber = page.CurrentPageNumber;
            var pageSize = page.PageSize;
            var name = m_facade.GetOrganizationById(id).Name;


            if (Request.Cookies["sort"] != null)
            {
                var sortType = Request.Cookies["sort"].Value;
                if (sortType == "descending")
                {
                    var sortedDepartments =
                        from department in m_facade.GetDepartmentsForOnePage(currentPageNumber, pageSize, "DESC")
                        where department.ParentOrganization.Id == id
                        select new DepartmentViewModel() { Name = department.Name, Id = department.Id };
                    return View(new OrganizationWithDepartmentsViewModel() { Id = id, Departments = sortedDepartments.ToList(), Name = name });
                }

                if (sortType == "ascending")
                {
                    var sortedDepartments =
                       from department in m_facade.GetDepartmentsForOnePage(currentPageNumber, pageSize, "ASC")
                       where department.ParentOrganization.Id == id
                       select new DepartmentViewModel() { Name = department.Name, Id = department.Id };
                    return View(new OrganizationWithDepartmentsViewModel() { Id = id, Departments = sortedDepartments.ToList(), Name = name });
                }
            }

            var defaultSortedDepartments =
                  from department in m_facade.GetDepartmentsForOnePage(currentPageNumber, pageSize, "DESC")
                  where department.ParentOrganization.Id == id
                  select new DepartmentViewModel() { Name = department.Name, Id = department.Id };
            return View(new OrganizationWithDepartmentsViewModel() { Id = id, Departments = defaultSortedDepartments.ToList(), Name = name });

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
            var organizationWithDepartmentsModel = new OrganizationWithDepartmentsViewModel() { Id = id, Departments = null, Name = name };
            return View(organizationWithDepartmentsModel);
        }

        public ActionResult UpdateOrganization(OrganizationWithDepartmentsViewModel organization)
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
