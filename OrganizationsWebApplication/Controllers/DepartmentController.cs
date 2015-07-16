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
    public class DepartmentController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
    
        public ActionResult UpdateDepartmentMenu(int id)
        {
            var department = m_facade.GetDepartmentById(id);
            var departmentModel = new DepartmentViewModel()
            {
                Id = department.Id,
                ParentId = department.ParentOrganization.Id,
                Name = department.Name
            };
            return View(departmentModel);
        }

        public ActionResult UpdateDepartment(DepartmentViewModel department)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DepartmentInfo(int id = 0)
        {
            Page page = Paginator.GetEmployeesListPage(m_facade, id);

            var name = m_facade.GetDepartmentById(id).Name;
            var employees =
               from employee in m_facade.GetEmployeesForOnePage(page.CurrentPageNumber, page.PageSize, page.CurrentInstanceId)
               select new EmployeeViewModel(){ Name = String.Join(" ", employee.Name, employee.LastName), Id = employee.Id };

            return View(new DepartmentWithEmployeesViewModel() { Id = id, Employees = employees.ToList(), Name = name, Page = page});
        }

        public ActionResult AddDepartmentMenu(int id)
        {
            var departmentModel = new DepartmentViewModel() { ParentId = id };
            return View(departmentModel);
        }

        public ActionResult AddDepartment(DepartmentViewModel department)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });

            return RedirectToAction("OrganizationInfo", "Organization", new { id = organization.Id } );
        }

        public ActionResult DeleteDepartment(int id = 0)
        {
            var parentId = m_facade.GetDepartmentById(id).ParentOrganization.Id;
            m_facade.DeleteDepartment(id);
         
            return RedirectToAction("OrganizationInfo", "Organization", new { id = parentId });
        }
    }
}
