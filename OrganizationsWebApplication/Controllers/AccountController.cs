using System;
using System.Web.Mvc;
using OrganizationsWebApplication.Models;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Administration()
        {
            Response.Redirect("~/home/index");
            return View();
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
            if (account.UserName.Length == 0 || account.Password.Length == 0)
            {
                Response.Redirect("~/account/login");
            }

            bool success = WebSecurity.Login(account.UserName, account.Password, false);
            if (success)
            {
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null)
                {
                    Response.Redirect("~/home/index");
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
        public ActionResult Register(Account account)
        {
            if (account.UserName.Length == 0 || account.Password.Length == 0)
            {
                Response.Redirect("~/account/register");
            }
            try
            {
                WebSecurity.CreateUserAndAccount(account.UserName, account.Password);
                if (account.UserName == "admin")
                {
                    var role = System.Web.Security.Roles.Provider;
                    role.AddUsersToRoles(new[] { account.UserName }, new[] { "admin" });
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/account/register");
            }

            Response.Redirect("~/home/index");
            return View();
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            Response.Redirect("~/account/login");
            return View();
        }
    }
}
