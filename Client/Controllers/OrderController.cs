using Client.Models.UserSettings;
using Client.Providers;
using Client.ViewModels.Order;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult OrderList()
        {
            if (!CurrentUser.isAuthenticated())
                return RedirectToAction("Login", "Auth");
            var response = RequestProvider.CallGetMethod("api/Order/List");
            OrderTableVM model = new OrderTableVM();
            List<OrderViewModel> orderList = new List<OrderViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                orderList = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);
            }
            model.Orders = orderList;
            model.UserEmail = CurrentUser.GetEmail;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel model)
        {
            if (!CurrentUser.isAuthenticated())
                return RedirectToAction("Login", "Auth");
            var response = RequestProvider.CallPostMethodJson("api/Order/Create", model);
            if (response.IsSuccessStatusCode)
            {                
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}