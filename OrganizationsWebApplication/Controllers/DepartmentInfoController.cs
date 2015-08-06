using System.Web.Mvc;
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
            int pageNumberInDepartmentInfo)
        {
            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                parentId,
                departmentId,
                m_facade.GetDepartmentById(departmentId).Name,
                pageNumberInOrganizationsList,
                pageNumberInOrganizationInfo,
                pageNumberInDepartmentInfo
                );
            //var sortedEmployees = SortingHelper.GetListSortedByName(employees);
            return View(model);
        }

        
        public ActionResult GoNextPage(
            int departmentId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo)
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
             nextPage
         );
            return View("DepartmentInfo", model);
        }

        
        public ActionResult GoPrevPage(
      int departmentId,
      int pageNumberInOrganizationsList,
      int pageNumberInOrganizationInfo,
      int pageNumberInDepartmentInfo)
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
             prevPage
         );
            return View("DepartmentInfo", model);
        }

        
        public ActionResult UpdateEmployeeMenu(
            int id,
            string name,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo)
        {
            var employeeModel = new EmployeeViewModel()
            {
                Id = id,
                Name = name,
                PageNumberInOrganizationsList = pageNumberInOrganizationsList,
                PageNumberInOrganizationInfo = pageNumberInOrganizationInfo,
                PageNumberInDepartmentInfo = pageNumberInDepartmentInfo
            };
            return View(employeeModel);
        }

        public ActionResult UpdateEmployee(EmployeeViewModel employee)
        {
            var employeeBm = m_facade.GetEmployeeById(employee.Id);
            employeeBm.Name = employee.Name;
            m_facade.UpdateEmployee(employeeBm);

            var model = new DepartmentWithEmployeesViewModel(
                m_facade,
                  employeeBm.ParentDepartment.ParentOrganization.Id,
                  employeeBm.ParentDepartment.Id,
                  employeeBm.ParentDepartment.Name,
                  employee.PageNumberInOrganizationsList,
                  employee.PageNumberInOrganizationInfo,
                  employee.PageNumberInDepartmentInfo
              );

            return View("DepartmentInfo", model);
        }

        public ActionResult AddEmployeeMenu(
            int departmentId,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo)
        {
            var employeeModel = new EmployeeViewModel()
            {
                ParentId = departmentId,
                PageNumberInOrganizationsList = pageNumberInOrganizationsList,
                PageNumberInOrganizationInfo = pageNumberInOrganizationInfo,
                PageNumberInDepartmentInfo = pageNumberInDepartmentInfo
            };
            return View(employeeModel);
        }

        public ActionResult AddEmployee(EmployeeViewModel employee)
        {
            var departmentBm = m_facade.GetDepartmentById(employee.ParentId);
            m_facade.AddEmployee(new Employee(0, departmentBm) { Name = employee.Name });

            var model = new DepartmentWithEmployeesViewModel(
         m_facade,
         departmentBm.ParentOrganization.Id,
         departmentBm.Id,
         departmentBm.Name,
         employee.PageNumberInOrganizationsList,
         employee.PageNumberInOrganizationInfo,
         employee.PageNumberInDepartmentInfo
         );

            return View("DepartmentInfo", model);
        }

        public ActionResult DeleteEmployee(
            int id,
            int pageNumberInOrganizationsList,
            int pageNumberInOrganizationInfo,
            int pageNumberInDepartmentInfo)
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
                pageNumberInDepartmentInfo);

            return View("DepartmentInfo", model);
        }
    }
}
