using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        
        public ActionResult Index()
        {
            return View();
        }
        
        ////////////////////////////////////////////////////

        public ActionResult UpdateDepartmentMenu(int id)
        {
            var department = m_facade.GetDepartmentById(id);
            var dtoDepartment = new DtoDepartment()
            {
                Id = department.Id,
                ParentId = department.ParentOrganization.Id,
                Name = department.Name
            };
            return View(dtoDepartment);
        }

        public ActionResult UpdateDepartment(DtoDepartment department)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DepartmentInfo(int id = 0)
        {
            var name = m_facade.GetDepartmentById(id).Name;
            var employees =
               from employee in m_facade.GetEmployeesInDepartment(id)
               select new DtoEmployee()
               {
                   Name = String.Join(" ", employee.Name, employee.LastName),
                   Id = employee.Id
               };

            return View(new DepartmentModel() { Id = id, Employees = employees.ToList(), Name = name });
        }

        public ActionResult AddDepartmentMenu(int id)
        {
            var department = new DtoDepartment() { ParentId = id };
            return View(department);
        }

        public ActionResult AddDepartment(DtoDepartment department)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });

            return RedirectToAction("OrganizationInfo", "Organization", new { id = organization.Id } );
        }

        public ActionResult DeleteDepartment(int id = 0)
        {
            var parentId = m_facade.GetDepartmentById(id).ParentOrganization.Id;
            m_facade.DeleteDepartment(id);
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("OrganizationInfo", "Organization", new { id = parentId });
        }

        

    }
}
