using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;
using Organizations.Container;
using OrganizationsWebApplication.Helpers;
using OrganizationsWebApplication.IoC;
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
        private Facade m_facade = ContainerWrapper.Container.GetInstance<Facade>();

        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public JsonResult IsImageForUserExists(int id)
        {
            bool result = false;
            var fileObject = MvcContainer.Container.With("id").EqualTo(id).GetInstance<ImageObject>();
            try
            {
                result = fileObject.IsImageForUserExists();
            }
            catch (Exception)
            {
                result = false;
            }
            return Json(result);
        }
        
        public FileResult GetImageFileByUserId(int id)
        {
            var fileObject = MvcContainer.Container.With("id").EqualTo(id).GetInstance<ImageObject>();

            try
            {
                return fileObject.GetImage();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return null;
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
        
        public JsonResult GetUser(int id)
        {
            var userBm = m_facade.GetEmployeeById(id);
            var user = new
            {
                userBm.Name, 
                userBm.Id, 
                DepartmentName = userBm.ParentDepartment.Name, 
                OrganizationName = userBm.ParentDepartment.ParentOrganization.Name
            };

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserProfileAngular(int id, ViewCondition viewCondition)
        {
            ViewData["id"] = id.ToString();
            return View();
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

            m_facade.DeleteEmployee(id);
            var fileObject = MvcContainer.Container.With("id").EqualTo(id).GetInstance<ImageObject>();

            try
            {
                fileObject.DeleteImage();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

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
                var fileObject = MvcContainer.Container.With("id").EqualTo(id).GetInstance<ImageObject>();

                try
                {
                    fileObject.SaveImage(file);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            return RedirectToAction("UserProfile", "Administration", new { id });
        }

        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            if (id != 0)
            {
                var fileObject = MvcContainer.Container.With("id").EqualTo(id).GetInstance<ImageObject>();

                try
                {
                    fileObject.DeleteImage();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            return RedirectToAction("UserProfile", "Administration", new { id });
        }
    }
}
