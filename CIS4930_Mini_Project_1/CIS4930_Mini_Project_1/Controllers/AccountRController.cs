using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
                        if(data[i].hashedkey == model.password)
                            return RedirectToAction("Index", "Home", model);

                // //temporary:
                // var userTable = new cis4930db.USERSDataTable();
                // usersAgent.Fill(userTable);
                // for (int i = 0; i < userTable.Count; i++)
                // {
                //     if (userTable[i].username == model.username)
                //     {
                //         if (userTable[i].hashedkey == model.password)
                //         {
                //             //login success
                //             
                //         }
                //     }
                // }
                ModelState.AddModelError("username","sorry account details don't match!");
            }

            return View(model);
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

        public ActionResult Test()
        {
            var sqlquery = "jose' OR 1=1) --";
            var db = new dbEntities();
            var data = db.Database.SqlQuery<USER>("SELECT [index], id, username, name, hashedkey FROM dbo.USERS WHERE (username = '"+sqlquery+"')").ToList<USER>();
            ViewBag.Data = data.Count;
            return View();
        }

        [HttpPost]
        public ActionResult CompleteToDo(int index, bool isComplete)
        {
            var table = todoAgent.GetDataByIndex(index);
            todoAgent.UpdateQuery(isComplete, index);
            return null;
        }

        // public ActionResult InCompleteTodo()
        // {
        //
        // }
    }

    
}