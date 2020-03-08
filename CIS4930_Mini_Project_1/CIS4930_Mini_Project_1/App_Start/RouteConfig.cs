using System.Web.Mvc;
using System.Web.Routing;

namespace CIS4930_Mini_Project_1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AccountR", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
