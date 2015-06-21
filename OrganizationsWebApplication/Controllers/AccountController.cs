using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace OrganizationsWebApplication.Controllers
{
    public class AccountController : Controller
    {

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
        public ActionResult Login(FormCollection form)
        {
            if (form["username"].Length == 0 || form["password"].Length == 0)
            {
                Response.Redirect("~/account/login");
            }

            bool success = WebSecurity.Login(form["username"], form["password"], false);
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
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            if (form["username"].Length == 0 || form["password"].Length == 0)
            {
                Response.Redirect("~/account/register");
            }
            try
            {
                WebSecurity.CreateUserAndAccount(form["username"], form["password"]);
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
