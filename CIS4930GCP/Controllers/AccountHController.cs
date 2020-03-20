using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using CIS4930GCP.App_Start;
using CIS4930GCP.Models;
using CIS4930GCP.Models.cis4930dbTableAdapters;

namespace CIS4930GCP.Controllers
{
    //TODO:: .Try doing an authorized state for the user. //

    public class AccountHController : Controller
    {
        USERSTableAdapter usersAgent = new USERSTableAdapter();
        TODOLISTTableAdapter todoAgent = new TODOLISTTableAdapter();
        CREDENTIALSEASONINGTableAdapter seasonAdapter = new CREDENTIALSEASONINGTableAdapter();

        public ActionResult Register()
        {
            return View();
        }
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

                //TODO:: STORE THE SALT IN THE DB
                string saltSeasoning = GetSalt();
                //Anthony will make the gethashed pass return the salted
                string hashedPassword = HashSaltPepperPassword(model.password, saltSeasoning);

                seasonAdapter.Insert(model.username, saltSeasoning, "default");
                //usersAgent.Insert("tbd", model.username, model.name, hashedPassword);
                usersAgent.Insert("tbd", model.username, model.name, hashedPassword);
                return RedirectToAction("Login", "AccountH");
            }
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
                var seasonTable = new cis4930db.CREDENTIALSEASONINGDataTable();

                //TODO:: use the value
                string saltForThisUserName = "";
                //saltForThisUserName has the salt you need. Anthony will do his magic with it.


                seasonAdapter.FillBy(seasonTable, model.username);
                usersAgent.Fill(userTable);
                if (userTable.Count!=0 & seasonTable.Count!=0)
                {
                    for (int i = 0; i < userTable.Count; i++)
                    {
                        if (userTable[i].username == model.username)
                        {
                            ModelState.AddModelError("user", userTable[i].username);


                            saltForThisUserName = seasonTable[0].salt;
                            ModelState.AddModelError("salt", saltForThisUserName);
                            ModelState.AddModelError("pass", model.password);

                            if (DecypherPassword(model.password, userTable[i].hashedkey, saltForThisUserName))
                            {
                                //login success
                                AppState.isLoggedIn = true;
                                AppState.loggedInUserName = userTable[i].username;
                                return RedirectToAction("DashboardH", "Home");
                            }
                        }
                    }
                }
            }

            ModelState.AddModelError("username", "sorry account details don't match!");



            return View(model);
        }

        public ActionResult Logout()
        {
            AppState.Logout();
            return RedirectToAction("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTodo(TodoModel model)
        {
            if (AppState.isLoggedIn & model.username == AppState.loggedInUserName)
            {

                todoAgent.Insert(model.username, model.todo, false);
                return RedirectToAction("DashboardH", "Home");
            }
            AppState.Logout();
            return RedirectToAction("Error");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteToDo(TodoModel model)
        {

            if (AppState.isLoggedIn & model.username == AppState.loggedInUserName)
            {

                todoAgent.UpdateQuery(model.isComplete, model.index);
                return RedirectToAction("DashboardH", "Home");
            }

            AppState.Logout();
            return RedirectToAction("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteToDo(TodoModel model)
        {

            if (AppState.isLoggedIn & model.username == AppState.loggedInUserName)
            {

                todoAgent.DeleteQuery(model.index);
                return RedirectToAction("DashboardH", "Home");
            }

            AppState.Logout();
            return RedirectToAction("Error");
        }

        public string GetSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            Random rnd = new Random();
            byte[] buff = new byte[rnd.Next(1, 20)];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);

        }

        private string HashSaltPepperPassword(string password, string salt)
        {
            //Here you Hash it salt it and the return a encrypted password
            string pepper = "softwareSecurity";
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt + pepper);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool DecypherPassword(string plainPassword, string encryptedPassword, string salt)
        {
            string newHashedPin = HashSaltPepperPassword(plainPassword, salt);
            return newHashedPin.Equals(encryptedPassword);

        }


        public ActionResult Error()
        {
            return View();
        }
    }
}