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

            /* View routing. */
            routes.MapRoute(
                name: "View",
                url: "{cell}/{controller}/{id}",
                defaults: new { action = "View"},
                constraints: new
                {
                    cell = "^(?!.*(?i:Stories|Requirements|Revision|Baseline|Account)).*$",
                    id = "^(?!.*(?i:Index|View|Edit|Delete)).*$"
                }
            );

            /* Cells routing. */
            routes.MapRoute(
                name: "Cells",
                url: "{cell}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { cell = "^(?!.*(?i:Stories|Requirements|Revision|Baseline|Account)).*$" }
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