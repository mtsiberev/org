using System.Linq;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models.EntitiesModels;
using OrganizationsWebApplication.Models.PagesModels;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class DepartmentInfoController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        
        public ActionResult DepartmentInfo(int departmentId, int pageNumberInDepartmentInfo, string sortType)
        {
            var role = System.Web.Security.Roles.Provider;
                
            var departmentInfo = m_facade.GetDepartmentWithEmployees(departmentId, pageNumberInDepartmentInfo, sortType);
            var departmentInfoViewModel = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            var usersList = m_facade.GetAllEmployees().ToList();
            var freeUsersList =
                from user in usersList
                where (user.ParentDepartment == null) && (!role.IsUserInRole(user.Name, "admin"))
                
                select new EmployeeViewModel() { Id = user.Id, ParentId = 0, Name = user.Name };
            var freeUsersViewModel = new FreeUsersViewModel() { Content = freeUsersList.ToList() };

            departmentInfoViewModel.EmployeeViewModel = new EmployeeViewModel() { ParentId = departmentId };
            departmentInfoViewModel.FreeUsersViewModel = freeUsersViewModel;

            return View(departmentInfoViewModel);
        }

        public ActionResult ChangeSortType(int departmentId, int pageNumberInDepartmentInfo, string sortType)
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
            
            
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
        new { departmentId, pageNumberInDepartmentInfo, sortType = newSortType });
            
        }

        public ActionResult GoNextPage(int departmentId, int pageNumberInDepartmentInfo, string sortType)
        {
            var nextPage = pageNumberInDepartmentInfo + 1;
           
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
           new { departmentId, pageNumberInDepartmentInfo = nextPage, sortType });
        }

        public ActionResult GoPrevPage(int departmentId, int pageNumberInDepartmentInfo, string sortType)
        {
            var prevPage = pageNumberInDepartmentInfo - 1;
           
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
           new { departmentId, pageNumberInDepartmentInfo = prevPage, sortType });
        }

        public ActionResult UpdateEmployeeMenu(int id, string name, int pageNumberInDepartmentInfo, string sortType)
        {
            var employeeModel = new EmployeeViewModel() { Id = id, Name = name };
            return View(employeeModel);
        }

        public ActionResult UpdateEmployee(EmployeeViewModel employee, int pageNumberInDepartmentInfo, string sortType)
        {
            var employeeBm = m_facade.GetEmployeeById(employee.Id);
            employeeBm.Name = employee.Name;
            m_facade.UpdateEmployee(employeeBm);

            var department = m_facade.GetDepartmentById(employeeBm.ParentDepartment.Id);

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
           new { departmentId = department.Id, pageNumberInDepartmentInfo = 1, sortType });
        }

        public ActionResult AddEmployeeMenu(int departmentId, int pageNumberInDepartmentInfo, string sortType)
        {
            var employeeModel = new EmployeeViewModel() { ParentId = departmentId };
            return View(employeeModel);
        }

        public ActionResult AddEmployee(EmployeeViewModel employee, int pageNumberInDepartmentInfo, string sortType)
        {
            var department = m_facade.GetDepartmentById(employee.ParentId);
            m_facade.AddEmployee(new Employee(0, department) { Name = employee.Name });

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
               new { departmentId = department.Id, pageNumberInDepartmentInfo = 1, sortType });
        }

        //////////////////////////////////
        public ActionResult AddEmployeeFromList(EmployeeViewModel employeeViewModel, int pageNumberInDepartmentInfo, string sortType)
        {
            var employee = m_facade.GetEmployeeById(employeeViewModel.Id);
            var department = m_facade.GetDepartmentById(employeeViewModel.ParentId);
            employee.ParentDepartment = department;

            m_facade.UpdateEmployee(employee);

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
                new { departmentId = department.Id, pageNumberInDepartmentInfo, sortType });
        }
        //////////////////////////////////

        public ActionResult DeleteEmployee(int id, int pageNumberInDepartmentInfo, string viewType, string sortType)
        {
            var department = m_facade.GetEmployeeById(id).ParentDepartment;
            m_facade.DeleteEmployee(id);

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
              new { departmentId = department.Id, pageNumberInDepartmentInfo = 1, sortType });
        }
    }
}
