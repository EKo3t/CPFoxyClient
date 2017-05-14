using Client.Models.UserSettings;
using Client.ViewModels.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;

namespace Client.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("Auth")]
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            HttpResponseMessage tokenResponse = GetTokenResponse(model.Email, model.Password);
            if (tokenResponse.IsSuccessStatusCode)
            {
                string accessToken = tokenResponse.Content.ReadAsStringAsync().Result;
                Dictionary<string, string> tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(accessToken);
                CurrentUser.TryCreateInstance(model.Email, model.Password, tokenDictionary);
            }
            return RedirectToAction("Index", "Home");
        }

        private static HttpResponseMessage GetTokenResponse(string email, string password)
        {
            if (email == null || email == "" || password == null || password == "")
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "UserName", email ),
                            new KeyValuePair<string, string> ( "Password", password )
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync("http://localhost:53063/Token", content).Result;
                return response;
            }
        }

        public ActionResult Logout()
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Instance.AccessToken["access_token"]);
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "http://localhost:53063/api/Account/Logout");
                var response =
                    client.SendAsync(req).Result;
                if (response.IsSuccessStatusCode)
                {
                    CurrentUser.LogOut();
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterViewModel model)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response =
                        client.PostAsJsonAsync(
                        "http://localhost:53063/api/Account/Register",
                        model).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error while getting response");
                        return View();
                    }
                } catch(Exception ex)
                {
                    ModelState.AddModelError("", "Cannot connect to server");
                    return View(model);
                }
            }
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            return View();
        }


        [HttpPost]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!CurrentUser.IsAuthenticated)
                return RedirectToAction("Login", "Auth");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Instance.AccessToken["access_token"]);
                var response =
                    client.PostAsJsonAsync(
                    "http://localhost:53063/api/Account/ChangePassword",
                    model).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error while getting response");
                    return View();
                }
            }
        }
    }
}
