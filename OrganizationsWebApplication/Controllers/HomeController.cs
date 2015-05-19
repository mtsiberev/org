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
        ////////////////////////////////////////////////////
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
            return View(new OrganizationModel(){Id = id, Departments = departments.ToList(), Name = name});
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

        public ActionResult UpdateOrganizationMenu(int id)
        {
            var name = m_facade.GetOrganizationById(id).Name;
            var organization = new OrganizationModel(){ Id = id, Departments = null, Name = name };
            return View(organization);
        }
        
        public ActionResult UpdateOrganization(OrganizationModel organization)
        {
            m_facade.UpdateOrganization(organization.Id, new Organization(organization.Id) { Name = organization.Name});
            return RedirectToAction("Index");
        }
        
        public ActionResult DeleteOrganization(int id = 0)
        {
            m_facade.DeleteOrganization(id);
            return RedirectToAction("Index");
        }
        
        ////////////////////////////////////////////////////
        public ActionResult UpdateDepartmentMenu(int id)
        {
            var department = m_facade.GetDepartmentById(id);
            var dtoDepartment = new DtoDepartment() { Id = department.Id, 
                ParentId = department.ParentOrganization.Id, 
                Name = department.Name };
            return View(dtoDepartment);
        }

        public ActionResult UpdateDepartment(DtoDepartment department)
        {
            var departmentBm = m_facade.GetDepartmentById(department.Id);
            departmentBm.Name = department.Name;
            m_facade.UpdateDepartment(departmentBm.Id, departmentBm);
            return RedirectToAction("Index");
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

            return View(new DepartmentModel(){Id = id, Employees = employees.ToList(), Name = name});
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
            return RedirectToAction("Index");
        }

        public ActionResult DeleteDepartment(int id = 0)
        {
            m_facade.DeleteDepartment(id);
            return RedirectToAction("Index");
        }

        ///////////////////////////////////////////////////////
        public ActionResult UpdateEmployeeMenu(int id)
        {
            var employee = m_facade.GetEmployeeById(id);
            var dtoEmployee = new DtoEmployee()
            {
                Id = employee.Id,
                ParentId = employee.ParentDepartment.Id,
                Name = employee.Name
            };
            return View(dtoEmployee);
        }
        
        public ActionResult UpdateEmployee(DtoEmployee employee)
        {
            var employeeBm = m_facade.GetEmployeeById(employee.Id);
            employeeBm.Name = employee.Name;
            m_facade.UpdateEmployee(employeeBm.Id, employeeBm);
            return RedirectToAction("Index");
        }

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

        public ActionResult DeleteEmployee(int id = 0)
        {
            m_facade.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
