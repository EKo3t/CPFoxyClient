using Client.Providers;
using Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Tools
{
    public static class ServiceLoader
    {
        public static List<ServiceVM> GetAll()
        {
            var response = RequestProvider.CallGetMethod("api/Service/Get");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<List<ServiceVM>>(json);
                    return result;
                }
                catch (Exception ex)
                {
                    return Enumerable.Empty<ServiceVM>().ToList();
                }
            }
            return Enumerable.Empty<ServiceVM>().ToList();
        }

        public static List<SelectListItem> GetAllAsSelectList()
        {
            var result = new List<SelectListItem>();
            var list = GetAll();
            foreach (ServiceVM service in list)
            {
                result.Add(new SelectListItem
                {
                    Text = service.Name,
                    Value = service.Id.ToString()
                });
            }
            return result;
        }
    }
}