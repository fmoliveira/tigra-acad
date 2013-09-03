using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Tigra
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            /* Default API routing. */
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
