using System.Linq;
using System.Web.DynamicData;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.Models.EntitiesModels;
using OrganizationsWebApplication.Models.PagesModels;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize]
    public class DepartmentInfoController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult DepartmentInfo(int id, ViewState viewState)
        {
            var role = System.Web.Security.Roles.Provider;

            var departmentInfo = m_facade.GetDepartmentWithEmployees(id, viewState.CurrentPageNumber, viewState.SortType);
            var departmentInfoViewModel = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            var usersList = m_facade.GetAllEmployees().ToList();
            var freeUsersList =
                from user in usersList
                where (user.ParentDepartment == null) && (!role.IsUserInRole(user.Name, "admin"))
                
                select new EmployeeViewModel() { Id = user.Id, ParentId = 0, Name = user.Name };
            var freeUsersViewModel = new FreeUsersViewModel() { Content = freeUsersList.ToList() };

            departmentInfoViewModel.EmployeeViewModel = new EmployeeViewModel() { ParentId = id };
            departmentInfoViewModel.FreeUsersViewModel = freeUsersViewModel;

            return View(departmentInfoViewModel);
        }

        public ActionResult ChangeSortType(int id, ViewState viewState)
        {
            string newSortType = "asc";
            if (viewState.SortType == "desc")
            {
                newSortType = "asc";
            }
            else if (viewState.SortType == "asc")
            {
                newSortType = "desc";
            }
            
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
        new { id, viewState.CurrentPageNumber, SortType = newSortType });
            
        }

        public ActionResult GoNextPage(int id, ViewState viewState)
        {
            var nextPage = viewState.CurrentPageNumber + 1;
           
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
           new { id, CurrentPageNumber = nextPage, viewState.SortType });
        }

        public ActionResult GoPrevPage(int id, ViewState viewState)
        {
            var prevPage = viewState.CurrentPageNumber - 1;
           
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
           new { id, CurrentPageNumber = prevPage, viewState.SortType });
        }
        

        public ActionResult AddEmployeeMenu(int id, ViewState viewState)
        {
            var employeeModel = new EmployeeViewModel() { ParentId = id };
            return View(employeeModel);
        }

        public ActionResult AddEmployee(EmployeeViewModel employee, ViewState viewState)
        {
            var department = m_facade.GetDepartmentById(employee.ParentId);
            m_facade.AddEmployee(new Employee(0, department) { Name = employee.Name });

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
               new { id = department.Id, CurrentPageNumber = 1, viewState.SortType });
        }

        //////////////////////////////////
        public ActionResult AddEmployeeFromList(EmployeeViewModel employeeViewModel, ViewState viewState)
        {
            var employee = m_facade.GetEmployeeById(employeeViewModel.Id);
            var department = m_facade.GetDepartmentById(employeeViewModel.ParentId);
            employee.ParentDepartment = department;

            m_facade.UpdateEmployee(employee);

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
                new { id = department.Id, viewState.CurrentPageNumber, viewState.SortType });
        }
        //////////////////////////////////
        public ActionResult DeleteEmployee(int id, ViewState viewState)
        {
            var department = m_facade.GetEmployeeById(id).ParentDepartment;
            m_facade.DeleteEmployee(id);

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
              new { id = department.Id, CurrentPageNumber = 1, viewState.SortType });
        }
    }
}
