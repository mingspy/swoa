using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.MapHubs();
            RouteTable.Routes.MapConnection<YJ.Platform.SignalR.SignalRConnection>("yunjian", "yunjian");
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.RouteExistingFiles = false;
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                namespaces:new string[]{"WebMvc.Controllers"},
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}