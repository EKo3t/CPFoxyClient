using Client.Models.UserSettings;
using Client.Providers;
using Client.ViewModels;
using Client.ViewModels.Manager;
using Client.ViewModels.Order;
using Client.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ManagerController : Controller
    {
        public ActionResult Menu()
        {
            return View();
        }

        // GET: Manager
        [HttpGet]
        public ActionResult OrderPanel()
        {
            ManagerVM model = new ManagerVM();
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return RedirectToAction("Index", "Home");
            model.Orders = OrderLoader.GetOrderList();
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateDriver(string email)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return RedirectToAction("Index", "Home");
            var values = new Dictionary<string, string> { { "email", email } };
            var response = RequestProvider.CallPostMethodJson("api/Account/UsrInf", values);
            DriverVM model = new DriverVM();
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var userDetails = JsonConvert.DeserializeObject<UserDetails>(json);
                model = new DriverVM();
                model.UserDetails = new UserDetails();
                if (userDetails != null)
                {
                    model.UserDetails.Email = userDetails.Email;
                    model.UserDetails.FirstName = userDetails.FirstName;
                    model.UserDetails.LastName = userDetails.LastName;
                    model.UserDetails.MiddleName = userDetails.MiddleName;
                    model.UserDetails.BirthDate = userDetails.BirthDate;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateDriver(DriverVM model)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return RedirectToAction("Index", "Home");
            if (model.CarID == null)
            {
                ModelState.AddModelError("", "Выберите авто");
                return View(model);
            }
            var values = new Dictionary<string, string>
            {
                { "email", model.UserDetails.Email },
                { "car", model.CarID.ToString() }
            };
            var response = RequestProvider.CallPostMethodJson("api/Driver/Create", values);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DriverPanel()
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return RedirectToAction("Index", "Home");
            var response = RequestProvider.CallGetMethod("api/Driver/List");
            ManagerVM model = new ManagerVM();
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                model.Drivers = JsonConvert.DeserializeObject<List<DriverVM>>(json);
            }
            else
            {
                model.Drivers = Enumerable.Empty<DriverVM>().ToList();
            }
            return View(model);
        }

        public static List<CarVM> getCarList()
        {
            if (!CurrentUser.IsAuthenticated)
                return Enumerable.Empty<CarVM>().ToList();
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return Enumerable.Empty<CarVM>().ToList();
            try
            {
                var response = RequestProvider.CallGetMethod("api/Car/Get");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var list = JsonConvert.DeserializeObject<List<CarVM>>(json);
                    return list;
                }
                return Enumerable.Empty<CarVM>().ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<CarVM>().ToList();
            }
        }

        public static List<SelectListItem> getCarSelectList()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            List<CarVM> carList = getCarList();
            result.Add(new SelectListItem { Text = "Choose a car", Value = "null" });
            foreach (CarVM item in carList)
            {
                result.Add(new SelectListItem {
                    Text = item.CarMark + " " + item.CarModel + ", " + item.CarColor,
                    Value = item.Id.ToString()
                });
            }
            return result;
        }


        [HttpGet]
        public ActionResult CarPanel()
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return RedirectToAction("Index", "Home");
            ManagerVM model = new ManagerVM();
            model.Cars = getCarList();
            return View(model);
        }

        [HttpGet]
        public ActionResult ServicePanel()
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return RedirectToAction("Index", "Home");
            List<ServiceVM> serviceList = ServiceLoader.GetAll();
            ManagerVM managerModel = new ManagerVM();
            managerModel.Services = serviceList;
            return View(managerModel);
        }

        [HttpGet]
        public ActionResult CreateService()
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult CreateService(ServiceVM model)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            if (!CurrentUser.HasRole("Admin") || !CurrentUser.HasRole("Manager"))
                return RedirectToAction("Index", "Home");
            var response = RequestProvider.CallPostMethodJson("api/Service/Create", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ServicePanel", "Manager");
            }
            ModelState.AddModelError("", "Не все данные заполнены верно");
            return View(model);
        }
    }
}