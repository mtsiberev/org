using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using WebMatrix.WebData;

namespace OrganizationsWebApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            WebSecurity.InitializeDatabaseConnection("ConnectionString", "Users", "Id", "Name", autoCreateTables: true);
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            {
                var context = new HttpContextWrapper(HttpContext.Current);
                var routeData = RouteTable.Routes.GetRouteData(context);
                var culture = "en";
                if (routeData != null)
                {
                    if (routeData.Values["culture"] != null)
                    {
                        culture = routeData.Values["culture"].ToString();
                    }
                    try
                    {
                        var cultureInfo = new CultureInfo(culture);
                        Thread.CurrentThread.CurrentUICulture = cultureInfo;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }        
            }
        }
    }
}