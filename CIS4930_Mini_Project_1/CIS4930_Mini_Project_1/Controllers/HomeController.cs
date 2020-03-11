using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using CIS4930_Mini_Project_1.Models;

namespace CIS4930_Mini_Project_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(LoginModel model)
        {
            // var mvcName = typeof(Controller).Assembly.GetName();
            // var isMono = Type.GetType("Mono.Runtime") != null;
            //
            // ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            // ViewData["Runtime"] = isMono ? "Mono" : ".NET";
            ViewBag.id = model.username;
            return View();
        }

        public ActionResult AnotherPage()
        {
            return View();
        }

        public ActionResult Jose()
        {
            return View();
        }
    }
}
