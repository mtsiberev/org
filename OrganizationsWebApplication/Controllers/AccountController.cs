using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NLog;
using Organizations;
using OrganizationsWebApplication.Models;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private Logger m_logger = LogManager.GetCurrentClassLogger();
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult Administration()
        {
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public JsonResult IsUserNameExist(string userName)
        {
            var result = IsUserCreated(userName);
            return Json(!result);
        }
        
        [HttpPost]
        public JsonResult IsUserNameNotExist(string userName)
        {
            var result = IsUserCreated(userName);
            return Json(result);
        }

        private bool IsUserCreated(string userName)
        {
            var role = Roles.Provider;
            bool result;

            try
            {
                var user = role.GetRolesForUser(userName);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("UserDb", "Users", "Id", "UserName", autoCreateTables: true);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            bool success = WebSecurity.Login(account.UserName, account.Password, false);
            if (success)
            {
                return RedirectToAction("Index", "Home");
            }

            const string loginErrorMsg = "Login error";
            ViewData["error"] = loginErrorMsg;
            m_logger.Error(loginErrorMsg);
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("UserDb", "Users", "Id", "UserName", autoCreateTables: true);
            }

            var role = Roles.Provider;
            if (!role.RoleExists("admin"))
            {
                role.CreateRole("admin");
            }

            if (!role.RoleExists("user"))
            {
                role.CreateRole("user");
            }

            var organizationsList = m_facade.GetAllOrganizations().ToList();

            var orgList = new List<SelectListItem>();
            orgList.Add(new SelectListItem { Text = "Select a Organization", Value = "0" });

            foreach (var organization in organizationsList)
            {
                orgList.Add(new SelectListItem { Text = organization.Name, Value = organization.Id.ToString() });
            }

            ViewData["organization"] = orgList;
            return View();
        }


        public JsonResult GetDepartments(string id)
        {
            var departmentsList = m_facade.GetDepartmentsInOrganization(Int32.Parse(id));

            var states = new List<SelectListItem>();
            states.Add(new SelectListItem { Text = "Select", Value = "0" });

            foreach (var department in departmentsList)
            {
                states.Add(new SelectListItem { Text = department.Name, Value = department.Id.ToString() });
            }

            return Json(new SelectList(states, "Value", "Text"));
        }

        [HttpPost]
        public ActionResult RegisterFinishing(RegisterModel registerModel, string organization, string department)
        {
            int organizationId;
            int departmentId;
            if (Int32.TryParse(organization, out organizationId) && Int32.TryParse(department, out departmentId))
            {
                registerModel.OrganizationId = organizationId;
                registerModel.DepartmentId = departmentId;
            }

            try
            {
                if (registerModel.DepartmentId == 0)
                {
                    WebSecurity.CreateUserAndAccount(registerModel.UserName,
                        registerModel.Password);
                }

                else
                {
                    WebSecurity.CreateUserAndAccount(registerModel.UserName,
                        registerModel.Password,
                        propertyValues: new
                        {
                            DepartmentId = registerModel.DepartmentId
                        });
                }

                if (registerModel.UserName == "admin")
                {
                    var role = System.Web.Security.Roles.Provider;
                    role.AddUsersToRoles(
                        new[] { registerModel.UserName },
                        new[] { "admin" });
                }
                else
                {
                    var role = System.Web.Security.Roles.Provider;
                    role.AddUsersToRoles(
                        new[] { registerModel.UserName },
                        new[] { "user" });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Account");
            }

            var account = new Account() { UserName = registerModel.UserName, Password = registerModel.Password};
            return this.Login(account);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login", "Account");
        }
    }
}
