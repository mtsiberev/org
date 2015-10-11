using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Organizations;
using OrganizationsWebApplication.Models;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private Facade m_facade = RegisterByContainer.Container.GetInstance<Facade>();

        public ActionResult Administration()
        {
            return RedirectToAction("Index", "Home");
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
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Response.Redirect(returnUrl);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("UserDb", "Users", "Id", "UserName", autoCreateTables: true);
            }

            var role = System.Web.Security.Roles.Provider;
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
        public ActionResult RegisterFinishing(Account account, string organization, string department)
        {
            int organizationId;
            int departmentId;
            if (Int32.TryParse(organization, out organizationId) && Int32.TryParse(department, out departmentId))
            {
                account.OrganizationId = organizationId;
                account.DepartmentId = departmentId;
            }

            try
            {
                if (account.DepartmentId == 0)
                {
                    WebSecurity.CreateUserAndAccount(account.UserName,
                        account.Password);
                }

                else
                {
                    WebSecurity.CreateUserAndAccount(account.UserName,
                        account.Password,
                        propertyValues: new
                        {
                            DepartmentId = account.DepartmentId
                        });
                }

                if (account.UserName == "admin")
                {
                    var role = System.Web.Security.Roles.Provider;
                    role.AddUsersToRoles(
                        new[] {account.UserName},
                        new[] {"admin"});
                }
                else
                {
                    var role = System.Web.Security.Roles.Provider;
                    role.AddUsersToRoles(
                        new[] { account.UserName },
                        new[] { "user" });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Account");
            }

            return this.Login(account);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login", "Account");
        }
    }
}
