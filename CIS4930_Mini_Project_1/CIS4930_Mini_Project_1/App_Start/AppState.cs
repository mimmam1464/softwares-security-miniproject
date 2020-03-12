using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS4930_Mini_Project_1.App_Start
{
    public static class AppState
    {
        public static bool isLoggedIn = false;
        public static string loggedInUserName = null;

        public static void Login(string username)
        {
            isLoggedIn = true;
            loggedInUserName = username;
        }

        public static void Logout()
        {
            isLoggedIn = false;
            loggedInUserName = null;
        }

    }
}