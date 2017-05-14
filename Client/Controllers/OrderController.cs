using Client.Models.UserSettings;
using Client.Providers;
using Client.Tools;
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
            if (!CurrentUser.IsAuthenticated)
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
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            var response = RequestProvider.CallPostMethodJson("api/Order/Create", model);
            if (response.IsSuccessStatusCode)
            {                
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(OrderViewModel order, string email = null)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            order.Email = email == null ? CurrentUser.GetEmail : email;
            var response = RequestProvider.CallPostMethodJson("api/Order/Delete", order);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("OrderList", "Order");
            }
            return RedirectToAction("OrderList", "Order");                        
        }

        [HttpGet]
        public ActionResult Edit(OrderViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(OrderViewModel model)
        {
            var response = RequestProvider.CallPostMethodJson("api/Order/Update", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("OrderList", "Order");
            }
            else
                return RedirectToAction("Edit", model);
        }
    }
}