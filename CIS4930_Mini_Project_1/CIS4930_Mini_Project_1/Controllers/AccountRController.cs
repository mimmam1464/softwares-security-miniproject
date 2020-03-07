using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CIS4930_Mini_Project_1.Models;

namespace CIS4930_Mini_Project_1.Controllers
{
    public class AccountRController : Controller
    {
        public ActionResult Credential()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Credential (LoginModel model)
        {
            if (!model.isRegistration)
            {
                //This is for login model
                //Do input validation
                if (String.IsNullOrWhiteSpace(model.username))
                    ModelState.AddModelError("username", "Password cannot be null!");
                if (String.IsNullOrWhiteSpace(model.password))
                    ModelState.AddModelError("password", "Password cannot be null!");
                if (ModelState.IsValid)
                {
                    //Handle the login here
                    //temporary:
                    return RedirectToAction("Index","dashboard");
                }
            }

            return View(model);
        }

    }
}