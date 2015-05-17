using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using Organizations.DbEntity;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult Index()
        {
            var organizations =
                from organization in m_facade.GetAllOrganizations()
                where (true)
                select new DtoOrganization() { Name = organization.Name, Id = organization.Id };

            return View(new OrganizationListModels(organizations.ToList()));
        }

        public ActionResult OrganizationInfo(int id = 0)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var departments =
                from department in m_facade.GetAllDepartments()
                where department.ParentOrganization.Id == id
                select new DtoDepartment() { Name = department.Name, Id = department.Id };

            return View(new OrganizationModel(id, departments.ToList(), name));
        }

        public ActionResult AddOrganizationMenu()
        {
            return View();
        }

        public ActionResult AddOrganization(string name)
        {
            m_facade.AddOrganization(new Organization(0) { Name = name });
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteOrganization(int id = 0)
        {
            m_facade.DeleteOrganization(id);
            return RedirectToAction("Index");
        }
        
        ////////////////////////////////////////////////////
        
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

            return View(new DepartmentModel(id, employees.ToList(), name));
        }

        public ActionResult AddDepartmentMenu(int id)
        {
            var department = new DtoDepartment() { ParentId = id };
            return View(department);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddDepartment(DtoDepartment department)
        {
            var organization = m_facade.GetOrganizationById(department.ParentId);
            m_facade.AddDepartment(new Department(0, organization) { Name = department.Name });
            return RedirectToAction("Index");
        }

        ////////////////////////////////////////////////////////////////////////////
        
        public ActionResult AddEmployeeMenu(int id)
        {
            var employee = new DtoEmployee() { ParentId = id };
            return View(employee);
        }

        public ActionResult AddEmployee(DtoEmployee employee)
        {
            var department = m_facade.GetDepartmentById(employee.ParentId);
            m_facade.AddEmployee(new Employee(0, department) { Name = employee.Name });
            return RedirectToAction("Index");
        }
    }
}
