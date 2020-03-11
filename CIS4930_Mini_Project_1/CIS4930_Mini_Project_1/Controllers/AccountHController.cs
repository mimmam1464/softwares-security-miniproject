using System;
using System.Web.Mvc;
using CIS4930_Mini_Project_1.Models;
using CIS4930_Mini_Project_1.Models.cis4930dbTableAdapters;

namespace CIS4930_Mini_Project_1.Controllers
{
    //TODO:: .Try doing an authorized state for the user. //
  
    public class AccountHController : Controller
    {
        USERSTableAdapter usersAgent = new USERSTableAdapter();
        TODOLISTTableAdapter todoAgent = new TODOLISTTableAdapter();

        // GET: AccountH
        [HttpPost]
        public ActionResult Register(LoginModel model)
        {
            //when the user wants registration
            //This is for login model
            //Do input validation
            if (String.IsNullOrWhiteSpace(model.username))
                ModelState.AddModelError("username", "username cannot be null!");
            if (String.IsNullOrWhiteSpace(model.password))
                ModelState.AddModelError("password", "password cannot be null!");
            if (String.IsNullOrWhiteSpace(model.name))
                ModelState.AddModelError("name", "name cannot be null");
            if (String.IsNullOrWhiteSpace(model.confirmPassword))
                ModelState.AddModelError("confirmPassword", "password cannot be null!");
            if (model.password != model.confirmPassword)
                ModelState.AddModelError("confirmPassword", "passwords don't match!");
            if (ModelState.IsValid)
            {
                //Handle Registration Here//
                //TODO:: Password Hash, Salt and Pepper This

                usersAgent.Insert("tbd", model.username, model.name, model.password);
                return RedirectToAction("Login", "AccountR");
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            //This is for login model
            //Do input validation
            if (String.IsNullOrWhiteSpace(model.username))
                ModelState.AddModelError("username", "username cannot be null!");
            if (String.IsNullOrWhiteSpace(model.password))
                ModelState.AddModelError("password", "password cannot be null!");
            if (ModelState.IsValid)
            {
                //Login Handler
                //temporary:
                var userTable = new cis4930db.USERSDataTable();
                usersAgent.Fill(userTable);
                for (int i = 0; i < userTable.Count; i++)
                {
                    if (userTable[i].username == model.username)
                    {
                        //TODO:: dehash, desalt and depepper the password before the check
                        if (userTable[i].hashedkey == model.password)
                        {
                            //login success
                            AppData.LoggedIn = true;
                            AppData.loggedInUser = userTable[i].username;
                            return RedirectToAction("DashboardH","Home");
                        }
                    }
                }
                ModelState.AddModelError("username", "sorry account details don't match!");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO:: Make This Secured for CSRF//
        public ActionResult CompleteToDo(int index, bool isComplete)
        {
            var table = todoAgent.GetDataByIndex(index);
            todoAgent.UpdateQuery(isComplete, index);
            return null;
        }

        //TODO:: Going to the Dashboard do not allow if the id is not the user//
        private string HashSaltPepperPassword(string password)
        {
            //Here you Hash it salt it and the return a encrypted password
            return "";
        }

        private string DecypherPassword(string encryptedPassword)
        {
            return "";
        }
    }
}