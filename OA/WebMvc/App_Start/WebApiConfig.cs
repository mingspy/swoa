using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebMvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PlatformApi",
                routeTemplate: "PlatformApi/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            GlobalConfiguration.Configuration.Formatters[0] = new PlatformApiControllers.JsonNetFormatter();
        }
    }
}
