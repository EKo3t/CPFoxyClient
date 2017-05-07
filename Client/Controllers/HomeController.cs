using Client.Models.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home        
        public ActionResult Index()
        {
            if (!CurrentUser.isAuthenticated())
            {
                ViewBag.RedirectErrorMessage = "You are not logged in.";
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
    }
}