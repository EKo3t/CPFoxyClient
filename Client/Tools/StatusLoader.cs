using Client.Models;
using Client.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Tools
{
    public static class StatusLoader
    {
        public static List<OrderStatus> GetAll()
        {
            var response = RequestProvider.CallGetMethod("api/Status/GetAll");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<List<OrderStatus>>(json);
                    return result;
                }
                catch (Exception ex)
                {
                    return Enumerable.Empty<OrderStatus>().ToList();
                }
            }
            return Enumerable.Empty<OrderStatus>().ToList();
        }
    }
}