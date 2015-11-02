using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Organizations;
using OrganizationsWebApplication.Helpers;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.Models.EntitiesModels;
using NLog;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public FileResult GetImageFileByUserId(int id)
        {
            var fileDescription = ImageHelper.GetImageFileDescription(id);
            logger.Info("Getting file: '{0}' by user '{1}'", fileDescription.FileName, WebSecurity.CurrentUserName);

            return new FilePathResult(fileDescription.FilePath, fileDescription.ContentType);
        }
        
        public ActionResult AdministrationInfo(ViewCondition viewCondition)
        {
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

            return RedirectToAction("AdministrationInfo", "Administration",
                new { id, viewCondition.CurrentPageNumber, SortType = newSortType });
        }

        public ActionResult UserProfile(int id, ViewCondition viewCondition)
        {
            var employeeBm = m_facade.GetEmployeeById(id);


            var departmentName = "";
            var organizationName = "";

            if (employeeBm.ParentDepartment != null)
            {
                departmentName = employeeBm.ParentDepartment.Name;
                organizationName = employeeBm.ParentDepartment.ParentOrganization.Name;
            }

            var employeeModel = new EmployeeViewModel() { Id = id, Name = employeeBm.Name };

            var role = System.Web.Security.Roles.Provider;
            var userRole = role.GetRolesForUser(employeeModel.Name).First();
            employeeModel.Role = userRole;

            ViewData["organizationName"] = organizationName;
            ViewData["departmentName"] = departmentName;
            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult ChangeRoles(EmployeeViewModel employeeViewModel)
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
                logger.Info("User '{0}' deleted from '{1}' role.", employeeViewModel.Name, currentRole);

                role.AddUsersToRoles(
                    new[] { employeeViewModel.Name },
                    new[] { userRole }
                    );
                logger.Info("User '{0}' added to '{1}' role.", employeeViewModel.Name, currentRole);

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
            var userName = m_facade.GetEmployeeById(id).Name;

            ImageHelper.DeleteUserImageById(id);
            m_facade.DeleteEmployee(id);

            if (userName == WebSecurity.CurrentUserName)
            {
                WebSecurity.Logout();
                return RedirectToAction("Index", "Home",
                    new { id, viewCondition.CurrentPageNumber, viewCondition.SortType });
            }
            return RedirectToAction("AdministrationInfo", "Administration",
                new { id, viewCondition.CurrentPageNumber, viewCondition.SortType });
        }

        
        [HttpPost]
        public ActionResult SaveImage(HttpPostedFileBase file, int id)
        {
            if (file != null && file.ContentLength > 0)
            {
                ImageHelper.SaveUserImageById(file, id);
            }

            return RedirectToAction("UserProfile", "Administration", new { id } );
        }

        
        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            if (id != 0)
            {
                ImageHelper.DeleteUserImageById(id);
            }
      
            return RedirectToAction("UserProfile", "Administration", new { id });
        }
        
    }
}
