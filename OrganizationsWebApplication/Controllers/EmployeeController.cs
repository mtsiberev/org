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
            var employeeModel = new EmployeeViewModel()
            {
                Id = employee.Id,
                ParentId = employee.ParentDepartment.Id,
                Name = employee.Name
            };
            return View(employeeModel);
        }

        public ActionResult UpdateEmployee(EmployeeViewModel employee)
        {
            var employeeBm = m_facade.GetEmployeeById(employee.Id);
            employeeBm.Name = employee.Name;
            m_facade.UpdateEmployee(employeeBm);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddEmployeeMenu(int id)
        {
            var employeeModel = new EmployeeViewModel() { ParentId = id };
            return View(employeeModel);
        }

        public ActionResult AddEmployee(EmployeeViewModel employee)
        {
            var department = m_facade.GetDepartmentById(employee.ParentId);
            m_facade.AddEmployee(new Employee(0, department) { Name = employee.Name });

            return RedirectToAction("DepartmentInfo", "Department", new { id = department.Id } );
        }

        public ActionResult DeleteEmployee(int id = 0)
        {
            var parentId = m_facade.GetEmployeeById(id).ParentDepartment.Id;
            m_facade.DeleteEmployee(id);
          
            return RedirectToAction("DepartmentInfo", "Department", new { id = parentId });
        }
    }
}
