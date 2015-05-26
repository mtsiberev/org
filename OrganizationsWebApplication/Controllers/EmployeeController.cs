using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult Index()
        {
            return View();
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
            m_facade.UpdateEmployee(employeeBm);
            return RedirectToAction("Index", "Home");
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

            return RedirectToAction("DepartmentInfo", "Department", new { id = department.Id } );
        }

        public ActionResult DeleteEmployee(int id = 0)
        {
            m_facade.DeleteEmployee(id);
            return RedirectToAction("Index", "Home");
        }
  
    }
}
