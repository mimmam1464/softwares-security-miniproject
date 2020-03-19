using System;
using System.Linq;
using System.Web.Mvc;
using CIS4930_Mini_Project_1.App_Start;
using CIS4930_Mini_Project_1.Models;
using CIS4930_Mini_Project_1.Models.cis4930dbTableAdapters;

namespace CIS4930_Mini_Project_1.Controllers
{
    public class AccountRController : Controller
    {

        USERSTableAdapter usersAgent = new USERSTableAdapter();
        TODOLISTTableAdapter todoAgent = new TODOLISTTableAdapter();
        
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
                //Handle the login here
                //Entity Login
                //var sqlquery = "jose' OR 1=1) --";
                var db = new dbEntities();
                var data = db.Database.SqlQuery<USER>("SELECT [index], id, username, name, hashedkey FROM dbo.USERS WHERE (username = '" + model.username + "')").ToList<USER>();
                if (data.Count>0)
                    for(int i=0; i<data.Count; i++)
                        if (data[i].hashedkey == model.password)
                        {
                            AppState.Login(model.username);
                            return RedirectToAction("DashboardR", "Home");
                        }
                ModelState.AddModelError("username","sorry account details don't match!");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AppState.Logout();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

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
                //temporary:

                usersAgent.Insert("tbd", model.username, model.name, model.password);
                return RedirectToAction("Login", "AccountR", model);
            }
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddTodo(TodoModel model)
        {
            todoAgent.Insert(model.username, model.todo, false);
            return RedirectToAction("DashboardR","Home");
        }

        [HttpPost]
        public ActionResult CompleteToDo(TodoModel model)
        {
            todoAgent.UpdateQuery(model.isComplete, model.index);
            return RedirectToAction("DashboardR","Home");
        }

        public ActionResult DeleteToDo(TodoModel model)
        {
            todoAgent.DeleteQuery(model.index);
            return RedirectToAction("DashboardR", "Home");
        }

    }

    
}