using Client.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Client.Models.UserSettings
{
    public class CurrentUser
    {
        public static CurrentUser Instance { get; private set; }
        private static readonly string TokenKey = "";

        public Dictionary<string, string> AccessToken { get; private set; }
        private UserInfo info;
        public PageInfo lastPage { get; set; }

        private CurrentUser(string email, string password, Dictionary<string, string> accessToken)
        {
            info = new UserInfo(email, password);
            AccessToken = accessToken;
        }

        public static CurrentUser TryCreateInstance(string email, string password, Dictionary<string, string> accessToken)
        {
            if (Instance == null && accessToken != null && email != null && password != null)
                Instance = new CurrentUser(email, password, accessToken);
            return Instance;
        }

        public static void LogOut()
        {
            Instance = null;
        }

        public static bool isAuthenticated()
        {
            return Instance != null;
        }

        public static bool HasRole(String role)
        {
            using (var client = new HttpClient())
            {
                string url = "http://localhost:53063/api/Account/Role";
                var values = new Dictionary<string, string> { { "role", "Admin" } };
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CurrentUser.Instance.AccessToken["access_token"]);
                var response = client.PostAsJsonAsync(url, values).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
                }
                else
                    return false;
            }
            return false;
        }
    }
}