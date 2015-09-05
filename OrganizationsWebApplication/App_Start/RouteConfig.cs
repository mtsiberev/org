﻿using System.Web.Mvc;
using System.Web.Routing;

namespace OrganizationsWebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                 name: "OrganizationsList",
                 url: "{language}/{controller}/{action}",
                 defaults: new { language = "en", controller = "Home", action = "Index" }
             );
        }
    }
}