using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIS4930_Mini_Project_1.Models;

namespace CIS4930_Mini_Project_1.Controllers
{
    public class AccountRController : Controller
    {
        // GET: AccountR
        public ActionResult Register(LoginModel model)
        {
            //Do not check for any security issues.

            //Do input validation
            if(model.name == null)
                ModelState.AddModelError("name","Name cannot be null!");
            if (model.password == null)
                ModelState.AddModelError("name", "Password cannot be null!");

            //do null checks before registering in the database
            return null;
        }
    }
}