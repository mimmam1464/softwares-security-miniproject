using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CIS4930_Mini_Project_1
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
