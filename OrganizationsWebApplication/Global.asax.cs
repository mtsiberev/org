using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebMatrix.WebData;

namespace OrganizationsWebApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            WebSecurity.InitializeDatabaseConnection("ConnectionString", "Users", "Id", "Name", autoCreateTables: true);
        }


        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            {
                if (HttpContext.Current.Session != null)
                {
                    CultureInfo cultureInfo = (CultureInfo)this.Session["Culture"];

                    if (cultureInfo == null)
                    {
                        string langName = "en";

                        if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                        {
                            langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                        }
                        cultureInfo = new CultureInfo(langName);
                        this.Session["Culture"] = cultureInfo;
                    }
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                }
            }

        }

    }
}