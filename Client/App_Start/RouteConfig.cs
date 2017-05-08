using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Login", controller = "Auth", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Register",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Register", controller = "Auth", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ChangePassword",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "ChangePassword", controller = "Auth", id = UrlParameter.Optional }
            );

        }
    }
}

