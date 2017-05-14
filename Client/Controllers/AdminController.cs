using Client.Models.UserSettings;
using Client.Providers;
using Client.ViewModels;
using Client.ViewModels.Admin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin"))
                return RedirectToAction("Index", "Home");
            var model = new AdminPanelViewModel();
            model.UserList = getUserTableList();
            return View(model);
        }
        
        private List<UserTableModel> getUserTableList()
        {
            var response = RequestProvider.CallGetMethod("api/Admin/Users");
            if (response.IsSuccessStatusCode)
            {
                var list = JsonConvert.DeserializeObject<List<UserTableModel>>(response.Content.ReadAsStringAsync().Result);
                return list;
            }
            else
            {
                return new List<UserTableModel>();
            }
        }

        public ActionResult DeleteUser(String email)
        {
            if (!CurrentUser.IsAuthenticated)
                RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin"))
                return RedirectToAction("Index", "Home");
            var values = new Dictionary<string, string> { { "email", email } };
            var response = RequestProvider.CallPostMethodJson("api/Admin/Delete", values);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error while removing user. Check data correctness");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateUser(string email)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin"))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(UserCreateViewModel model)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin"))
                return RedirectToAction("Index", "Home");
            var response = RequestProvider.CallPostMethodJson("api/Admin/Create", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(String email)
        {
            if (!CurrentUser.IsAuthenticated)
                RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin"))
                return RedirectToAction("Index", "Home");
            UserDetails userDetails = new UserDetails();
            var values = new Dictionary<string, string> { { "email", email } };
            var response = RequestProvider.CallPostMethodJson("api/Account/UsrInf", values);
            if (response.IsSuccessStatusCode)
            {
                userDetails = JsonConvert.DeserializeObject<UserDetails>(response.Content.ReadAsStringAsync().Result);                
            }
            userDetails.Email = userDetails.Email == null ? email : userDetails.Email;
            return View(userDetails);
        }

        [HttpPost]
        public ActionResult Details(UserDetails model)
        {
            if (!CurrentUser.IsAuthenticated)
                RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin"))
                return RedirectToAction("Index", "Home");
            var response = RequestProvider.CallPostMethodJson("api/Account/ChangeInfo", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }
    }
}