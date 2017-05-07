using Client.Models.UserSettings;
using Client.Providers;
using Client.ViewModel.Admin;
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
        [HttpGet]
        // GET: Admin
        public ActionResult Index()
        {
            if (!CurrentUser.isAuthenticated())
                return RedirectToAction("Login", "Auth");
            var model = new AdminPanelViewModel();
            model.UserList = getUserTableList();
            return View(model);
        }
        
        private List<UserTableModel> getUserTableList()
        {
            var response = RequestProvider.CallGetMethod("api/Admin/Users");
            if (response.IsSuccessStatusCode)
            {
                string jsonFormatted = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                var list = JsonConvert.DeserializeObject<List<UserTableModel>>(jsonFormatted);
                return list;
            }
            else
            {
                return new List<UserTableModel>();
            }
        }

        public ActionResult DeleteUser()
        {

            return RedirectToAction("Index");
        }
    }
}