using Client.Models.UserSettings;
using Client.Providers;
using Client.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        [HttpGet]
        public ActionResult Create()
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin, Manager"))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Create(CarVM model)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin, Manager"))
                return RedirectToAction("Index", "Home");
            var response = RequestProvider.CallPostMethodJson("api/Car/Create", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CarPanel", "Manager");
            }
            return View(model);
        }
    }
}