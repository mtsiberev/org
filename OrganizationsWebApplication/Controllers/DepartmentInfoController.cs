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
            var departmentInfo = m_facade.GetDepartmentWithEmployees(departmentId, pageNumberInDepartmentInfo, sortType);
            var departmentInfoViewModel = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            var usersList = m_facade.GetAllEmployees().ToList();
            var freeUsersList =
                from user in usersList
                where user.ParentDepartment == null
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

            var departmentInfo = m_facade.GetDepartmentWithEmployees(departmentId, pageNumberInDepartmentInfo, newSortType);
            var model = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            return View("DepartmentInfo", model);
        }


        public ActionResult GoNextPage(int departmentId, int pageNumberInDepartmentInfo, string sortType)
        {
            var nextPage = pageNumberInDepartmentInfo + 1;
            var departmentInfo = m_facade.GetDepartmentWithEmployees(departmentId, nextPage, sortType);
            var model = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            return View("DepartmentInfo", model);
        }

        public ActionResult GoPrevPage(int departmentId, int pageNumberInDepartmentInfo, string sortType)
        {
            var prevPage = pageNumberInDepartmentInfo - 1;
            var departmentInfo = m_facade.GetDepartmentWithEmployees(departmentId, prevPage, sortType);
            var model = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            return View("DepartmentInfo", model);
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

            var departmentInfo = m_facade.GetDepartmentWithEmployees(employeeBm.ParentDepartment.Id, 1, sortType);
            var model = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            return View("DepartmentInfo", model);
        }

        public ActionResult AddEmployeeMenu(int departmentId, int pageNumberInDepartmentInfo, string sortType)
        {
            var employeeModel = new EmployeeViewModel() { ParentId = departmentId };
            return View(employeeModel);
        }

        public ActionResult AddEmployee(EmployeeViewModel employee, int pageNumberInDepartmentInfo, string sortType)
        {
            var departmentBm = m_facade.GetDepartmentById(employee.ParentId);
            m_facade.AddEmployee(new Employee(0, departmentBm) { Name = employee.Name });

            var departmentInfo = m_facade.GetDepartmentWithEmployees(departmentBm.Id, 1, sortType);
            var model = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            return View("DepartmentInfo", model);
        }

        //////////////////////////////////
        public ActionResult AddEmployeeFromList(DepartmentInfoViewModel departmentInfoViewModel)
        {
            var employeeBm = m_facade.GetEmployeeById(departmentInfoViewModel.EmployeeViewModel.Id);
            var department = m_facade.GetDepartmentById(departmentInfoViewModel.Id);
            employeeBm.ParentDepartment = department;

            m_facade.UpdateEmployee(employeeBm);

            var departmentInfo = m_facade.GetDepartmentWithEmployees(departmentInfoViewModel.Id, departmentInfoViewModel.CurrentPageNumber, departmentInfoViewModel.SortType);
            var model = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            return View("DepartmentInfo", model);
        }
        //////////////////////////////////

        public ActionResult DeleteEmployee(int id, int pageNumberInDepartmentInfo, string viewType, string sortType)
        {
            var department = m_facade.GetEmployeeById(id).ParentDepartment;
            m_facade.DeleteEmployee(id);

            var departmentInfo = m_facade.GetDepartmentWithEmployees(department.Id, 1, sortType);
            var model = EntitiesListToView.GetDepartmentInfoViewModel(departmentInfo);

            return View("DepartmentInfo", model);
        }
    }
}
