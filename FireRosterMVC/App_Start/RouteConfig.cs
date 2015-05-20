using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FireRosterMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Custom Routes 
            routes.MapRoute(
                name: "Address",
                url: "Staff/{staffId}/Address/{action}/{id}",
                defaults: new
                {
                    controller = "Address",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                );

            routes.MapRoute(
               name: "EmergencyContact",
               url: "Staff/{staffId}/EmergencyContact/{action}/{id}",
               defaults: new
               {
                   controller = "EmergencyContact",
                   action = "Index",
                   id = UrlParameter.Optional
               }
               );

            routes.MapRoute(
               name: "Phone",
               url: "Staff/{staffId}/Phone/{action}/{id}",
               defaults: new
               {
                   controller = "Phone",
                   action = "Index",
                   id = UrlParameter.Optional
               }
               );

            // Default Routes
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            

        }
    }
}
