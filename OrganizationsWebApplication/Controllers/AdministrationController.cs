using System;
using System.Linq;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.Models.EntitiesModels;
using NLog;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public ActionResult AdministrationInfo(ViewCondition viewCondition)
        {
            var role = System.Web.Security.Roles.Provider;

            var allUsers = m_facade.GetAllEmployees(viewCondition.CurrentPageNumber, viewCondition.SortType);
            var usersInfoViewModel = EntitiesListToView.GetUsersListViewModel(allUsers);

            return View(usersInfoViewModel);
        }

        public ActionResult GoNextPage(int id, ViewCondition viewCondition)
        {
            var nextPage = viewCondition.CurrentPageNumber + 1;

            return RedirectToAction("AdministrationInfo", "Administration",
           new { id, CurrentPageNumber = nextPage, viewCondition.SortType });
        }

        public ActionResult GoPrevPage(int id, ViewCondition viewCondition)
        {
            var prevPage = viewCondition.CurrentPageNumber - 1;

            return RedirectToAction("AdministrationInfo", "Administration",
           new { id, CurrentPageNumber = prevPage, viewCondition.SortType });
        }
        
        public ActionResult UserProfile(int id, ViewCondition viewCondition)
        {
            var employeeBm = m_facade.GetEmployeeById(id);
            var employeeModel = new EmployeeViewModel() { Id = id, Name = employeeBm.Name };

            var role = System.Web.Security.Roles.Provider;
            var userRole = role.GetRolesForUser(employeeModel.Name).First();

            employeeModel.Role = userRole;
            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult ChangeRoles(EmployeeViewModel employeeViewModel )
        {
            string userRole = employeeViewModel.Role;

            employeeViewModel.Name = m_facade.GetEmployeeById(employeeViewModel.Id).Name;
            
            try
            {
                var role = System.Web.Security.Roles.Provider;

                var currentRole = role.GetRolesForUser(employeeViewModel.Name);
                role.RemoveUsersFromRoles(
                    new[] { employeeViewModel.Name },
                    currentRole);
                
                role.AddUsersToRoles(
                    new[] { employeeViewModel.Name },
                    new[] { userRole }
                    );
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult DeleteEmployee(int id, ViewCondition viewCondition)
        {
            m_facade.DeleteEmployee(id);

            return RedirectToAction("AdministrationInfo", "Administration",
                new { id, viewCondition.CurrentPageNumber, viewCondition.SortType });
        }
        
    }
}
