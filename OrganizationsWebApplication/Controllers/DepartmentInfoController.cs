﻿using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class DepartmentInfoController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult DepartmentInfo(
            int parentId,
            int departmentId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                parentId,
                departmentId,
                m_facade.GetDepartmentById(departmentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                pageNumberInDepartmentInfo,
                viewType,
                sortType);
         
            return View(model);
        }
        
        public ActionResult ChangeViewType(
            int departmentId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            string newViewType = "list";
            if (viewType == "list")
            {
                newViewType = "grid";
            }
            else if (viewType == "grid")
            {
                newViewType = "list";
            }
            var department = m_facade.GetDepartmentById(departmentId);

            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                department.ParentOrganization.Id,
                department.Id,
                department.Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                pageNumberInDepartmentInfo,
                newViewType,
                sortType);
            
            return View("DepartmentInfo", model);
        }
        

        public ActionResult ChangeSortType(
            int departmentId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            string newSortType = "asc";
            if (sortType == "desc")
            {
                newSortType = "asc";
            }
            else if (sortType == "asc")
            {
                newSortType = "desc";
            }

            var department = m_facade.GetDepartmentById(departmentId);

            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                department.ParentOrganization.Id,
                department.Id,
                department.Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                pageNumberInDepartmentInfo,
                viewType,
                newSortType);

            return View("DepartmentInfo", model);
        }


        public ActionResult GoNextPage(
            int departmentId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            var nextPage = pageNumberInDepartmentInfo + 1;
            var department = m_facade.GetDepartmentById(departmentId);
            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                department.ParentOrganization.Id,
                department.Id,
                department.Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                nextPage,
                viewType,
                sortType);
            
            return View("DepartmentInfo", model);
        }
        
        public ActionResult GoPrevPage(
            int departmentId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            var prevPage = pageNumberInDepartmentInfo - 1;
            var department = m_facade.GetDepartmentById(departmentId);

            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                department.ParentOrganization.Id,
                department.Id,
                department.Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                prevPage,
                viewType,
                sortType);
     
            return View("DepartmentInfo", model);
        }
        
        public ActionResult UpdateEmployeeMenu(
            int id,
            string name,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            var employeeModel = new EmployeeViewModel()
            {
                Id = id,
                Name = name
            };
            return View(employeeModel);
        }

        public ActionResult UpdateEmployee(EmployeeViewModel employee,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            var employeeBm = m_facade.GetEmployeeById(employee.Id);
            employeeBm.Name = employee.Name;
            m_facade.UpdateEmployee(employeeBm);

            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                employeeBm.ParentDepartment.ParentOrganization.Id,
                employeeBm.ParentDepartment.Id,
                employeeBm.ParentDepartment.Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                pageNumberInDepartmentInfo,
                viewType,
                sortType);
            
            return View("DepartmentInfo", model);
        }

        public ActionResult AddEmployeeMenu(
            int departmentId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            var employeeModel = new EmployeeViewModel()
            {
                ParentId = departmentId
            };
            return View(employeeModel);
        }

        public ActionResult AddEmployee(
            EmployeeViewModel employee, 
            int pageNumberInOrganizationsList, 
            int pageNumberInOrganizationInfo, 
            int pageNumberInDepartmentInfo, 
            string viewType, 
            string sortType)
        {
            var departmentBm = m_facade.GetDepartmentById(employee.ParentId);
            m_facade.AddEmployee(new Employee(0, departmentBm) { Name = employee.Name });

            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                departmentBm.ParentOrganization.Id,
                departmentBm.Id,
                departmentBm.Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                pageNumberInDepartmentInfo,
                viewType,
                sortType);

            return View("DepartmentInfo", model);
        }

        public ActionResult DeleteEmployee(
            int id,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo,
            string viewType,
            string sortType)
        {
            var department = m_facade.GetEmployeeById(id).ParentDepartment;
            m_facade.DeleteEmployee(id);

            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                department.ParentOrganization.Id,
                department.Id,
                department.Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                pageNumberInDepartmentInfo,
                viewType,
                sortType);

            return View("DepartmentInfo", model);
        }
    }
}