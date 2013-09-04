using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tigra
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /* Cells routing. */
            routes.MapRoute(
                name: "Cells",
                url: "{cell}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", cell = "", action = "Index", id = UrlParameter.Optional }
            );

            /* Default page routing. */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", cell = "", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}