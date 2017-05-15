using Client.Models.UserSettings;
using Client.Providers;
using Client.Tools;
using Client.ViewModels.Manager;
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
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            OrderViewModel model = new OrderViewModel();
            model.Email = CurrentUser.GetEmail;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel model)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            try
            {
                model.OrderTime = DateTime.Parse(model.OrderTimeString);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Введите верный формат даты");
                return View(model);
            }
            var response = RequestProvider.CallPostMethodJson("api/Order/Create", model);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var guidString = JsonConvert.DeserializeObject<string>(json);
                model.Id = Guid.Parse(guidString);
                return RedirectToAction("Info", model);
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
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            var response = RequestProvider.CallPostMethodJson("api/Order/Update", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("OrderList", "Order");
            }
            else
                return RedirectToAction("Edit", model);
        }

        [HttpGet]
        public ActionResult Info(OrderViewModel model)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            OrderInfoVM infoVM = new OrderInfoVM();
            infoVM.OrderTime = model.OrderTime.ToString("dd:MM:yyyy HH:mm");
            var service = ServiceLoader.GetAll().FirstOrDefault(u => u.Id.Equals(model.ServiceId));
            infoVM.ServiceName = service == null ? null : service.Name;
            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                { "orderId", model.Id.ToString() }
            };
            var response = RequestProvider.CallPostMethodJson("api/Driver/Busy", values);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Title = "Заказ принят";
                string json = response.Content.ReadAsStringAsync().Result;
                var driverVM = JsonConvert.DeserializeObject<DriverVM>(json);
                infoVM.FullName = driverVM.UserDetails.LastName + " " +
                    driverVM.UserDetails.FirstName + " " +
                    driverVM.UserDetails.MiddleName;
                infoVM.CarSpec = driverVM.Car.CarMark + " " +
                    driverVM.Car.CarModel + ", " +
                    driverVM.Car.CarColor;
                return View(infoVM);
            }
            ViewBag.Title = "В заказе отказано";
            if (response.ReasonPhrase != null)
                ModelState.AddModelError("", "Нет свободных водителей");
            return View();
        }
    }
}