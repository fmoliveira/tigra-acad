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

            /* Details routing. */
            routes.MapRoute(
                name: "Details",
                url: "{cell}/{controller}/{tag}/{action}",
                defaults: new { controller = "Home", action = "Details" },
                constraints: new
                {
                    cell = "^(?!.*(?i:Stories|Requirements|Revision|Baseline|Account)).*$",
                    tag = "^(?!.*(?i:Index|Details|Create|Edit|Delete)).*$"
                }
            );

            /* Cells routing. */
            routes.MapRoute(
                name: "Cells",
                url: "{cell}/{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new
                {
                    cell = "^(?!.*(?i:Stories|Requirements|Revision|Baseline|Account)).*$"
                }
            );

            /* Default routing. */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            /* Menu routing. */
            routes.MapRoute(
                name: "Menu",
                url: "{cell}/{controller}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new
                {
                    cell = "^(?!.*(?i:Stories|Requirements|Revision|Baseline|Account)).*$"
                }
            );

            /* Admin routing. */
            routes.MapRoute(
                name: "Admin",
                url: "Admin/{controller}/{action}",
                defaults: new { action = "Index" }
            );
        }
    }
}