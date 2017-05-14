using Client.Models.UserSettings;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Client.Providers
{
    public class RequestProvider
    {
        private static readonly string ServerAdress = "http://localhost:53063/";

        public static HttpResponseMessage CallGetMethod(string address)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CurrentUser.Instance.AccessToken["access_token"]);
                var response = client.GetAsync(ServerAdress + address).Result;
                return response;
            }            
        }

        public static HttpResponseMessage CallPostMethodJson<T>(string address, T obj)
        {
            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(obj);
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CurrentUser.Instance.AccessToken["access_token"]);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest };
                }
                var response = client.PostAsJsonAsync(ServerAdress + address, obj).Result;
                return response;
            }
        }
    }
}   