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

        public ActionResult DepartmentInfo(int id, ViewCondition viewCondition)
        {
            var role = System.Web.Security.Roles.Provider;

            var departmentInfo = m_facade.GetDepartmentWithEmployees(id, viewCondition.CurrentPageNumber, viewCondition.SortType);
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

        public ActionResult ChangeSortType(int id, ViewCondition viewCondition)
        {
            string newSortType = "asc";
            if (viewCondition.SortType == "desc")
            {
                newSortType = "asc";
            }
            else if (viewCondition.SortType == "asc")
            {
                newSortType = "desc";
            }
            
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
        new { id, viewCondition.CurrentPageNumber, SortType = newSortType });
            
        }

        public ActionResult GoNextPage(int id, ViewCondition viewCondition)
        {
            var nextPage = viewCondition.CurrentPageNumber + 1;
           
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
           new { id, CurrentPageNumber = nextPage, viewCondition.SortType });
        }

        public ActionResult GoPrevPage(int id, ViewCondition viewCondition)
        {
            var prevPage = viewCondition.CurrentPageNumber - 1;
           
            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
           new { id, CurrentPageNumber = prevPage, viewCondition.SortType });
        }
        

        public ActionResult AddEmployeeMenu(int id, ViewCondition viewCondition)
        {
            var employeeModel = new EmployeeViewModel() { ParentId = id };
            return View(employeeModel);
        }

        public ActionResult AddEmployee(EmployeeViewModel employee, ViewCondition viewCondition)
        {
            var department = m_facade.GetDepartmentById(employee.ParentId);
            m_facade.AddEmployee(new Employee(0, department) { Name = employee.Name });

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
               new { id = department.Id, CurrentPageNumber = 1, viewCondition.SortType });
        }

        //////////////////////////////////
        public ActionResult AddEmployeeFromList(EmployeeViewModel employeeViewModel, ViewCondition viewCondition)
        {
            var employee = m_facade.GetEmployeeById(employeeViewModel.Id);
            var department = m_facade.GetDepartmentById(employeeViewModel.ParentId);
            employee.ParentDepartment = department;

            m_facade.UpdateEmployee(employee);

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
                new { id = department.Id, viewCondition.CurrentPageNumber, viewCondition.SortType });
        }
        //////////////////////////////////
        public ActionResult DeleteEmployee(int id, ViewCondition viewCondition)
        {
            var department = m_facade.GetEmployeeById(id).ParentDepartment;
            m_facade.DeleteEmployee(id);

            return RedirectToAction("DepartmentInfo", "DepartmentInfo",
              new { id = department.Id, CurrentPageNumber = 1, viewCondition.SortType });
        }
    }
}
