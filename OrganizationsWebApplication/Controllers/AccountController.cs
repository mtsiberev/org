using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Organizations;
using OrganizationsWebApplication.Mappers;
using OrganizationsWebApplication.Models;
using OrganizationsWebApplication.Models.EntitiesModels;
using OrganizationsWebApplication.Models.PagesModels;
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

            return View();
        }

        [HttpPost]
        public ActionResult RegisterChooseOrganization(Account account)
        {
            var organizationsList = m_facade.GetAllOrganizations().ToList();
            var organizationsViewModelsList =
                from organization in organizationsList
                select new OrganizationViewModel() { Id = organization.Id, Name = organization.Name };

            var registerViewModel = new RegisterViewModel() { Account = account, OrganizationsList = organizationsViewModelsList.ToList() };

            return View(registerViewModel);

        }

        [HttpPost]
        public ActionResult RegisterChooseDepartment(RegisterViewModel registerViewModel)
        {
            var departmentsList = m_facade.GetDepartmentsInOrganization(registerViewModel.Account.OrganizationId);
            var departmentsViewModelsList =
                from department in departmentsList
                select new DepartmentViewModel() { Id = department.Id, Name = department.Name };

            registerViewModel.DepartmentsList = departmentsViewModelsList.ToList();

            return View(registerViewModel);
        }

        [HttpPost]
        public ActionResult RegisterFinishing(RegisterViewModel registerViewModel)
        {
            try
            {
                if (registerViewModel.Account.DepartmentId == 0)
                {
                    WebSecurity.CreateUserAndAccount(registerViewModel.Account.UserName,
                        registerViewModel.Account.Password);
                }

                else
                {
                    WebSecurity.CreateUserAndAccount(registerViewModel.Account.UserName,
                        registerViewModel.Account.Password,
                        propertyValues: new
                        {
                            DepartmentId = registerViewModel.Account.DepartmentId
                        });
                }

                if (registerViewModel.Account.UserName == "admin")
                {
                    var role = System.Web.Security.Roles.Provider;
                    role.AddUsersToRoles(new[] { registerViewModel.Account.UserName }, new[] { "admin" });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Account");
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login", "Account");
        }
    }
}
