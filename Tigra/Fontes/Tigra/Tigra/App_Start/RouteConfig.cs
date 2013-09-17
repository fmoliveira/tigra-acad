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
                url: "{cell}/{controller}/{id}/{action}",
                defaults: new { controller = "Home", action = "Details" },
                constraints: new
                {
                    cell = "^(?!.*(?i:Stories|Requirements|Revision|Baseline|Account)).*$",
                    id = "^(?!.*(?i:Index|Details|Create|Edit|Delete)).*$"
                }
            );

            /* Default routing. */
            routes.MapRoute(
                name: "Default",
                url: "{cell}/{controller}/{action}",
                defaults: new { cell = UrlParameter.Optional, controller = "Home", action = "Index" },
                constraints: new
                {
                    cell = "^(?!.*(?i:Stories|Requirements|Revision|Baseline|Account)).*$"
                }
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
        }
    }
}