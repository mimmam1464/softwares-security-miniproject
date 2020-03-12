
using System.Web.Mvc;
using CIS4930_Mini_Project_1.App_Start;

namespace CIS4930_Mini_Project_1.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult DashboardR()
        {
            if (AppState.isLoggedIn)
            {
                ViewBag.id = AppState.loggedInUserName;
                ViewData["id"] = AppState.loggedInUserName;
                return View();
            }

            return RedirectToAction("Error");
        }
        public ActionResult DashboardH()
        {
            if (AppState.isLoggedIn)
            {
                ViewBag.id = AppState.loggedInUserName;
                ViewData["id"] = AppState.loggedInUserName;
                return View();
            }

            return RedirectToAction("Error");

        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
