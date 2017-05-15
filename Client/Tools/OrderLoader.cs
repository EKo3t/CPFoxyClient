using Client.Providers;
using Client.ViewModels.Order;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Tools
{
    public class OrderLoader
    {
        public static List<OrderViewModel> GetOrderList()
        {
            var response = RequestProvider.CallGetMethod("api/Order/AllList");
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                try
                {
                    var list = JsonConvert.DeserializeObject<List<OrderViewModel>>(json);
                    return list;
                }
                catch (Exception ex)
                {
                    return Enumerable.Empty<OrderViewModel>().ToList();
                }
            }
            return Enumerable.Empty<OrderViewModel>().ToList();
        }

        public static string GetCharListJson()
        {
            List<OrderViewModel> orderList = GetOrderList();
            double count = orderList.Count;
            Dictionary<Guid, int> serviceDict = new Dictionary<Guid, int>();
            Dictionary<Guid, string> serviceNameDict = new Dictionary<Guid, string>();
            foreach (OrderViewModel order in orderList)
            {
                if (!serviceDict.ContainsKey(order.Service.Id))
                {
                    serviceDict.Add(order.Service.Id, 0);
                    serviceNameDict.Add(order.Service.Id, order != null && order.Service != null ? order.Service.Name : "");
                }
                serviceDict[order.Service.Id] += 1;
            }
            List<PieChartModel> resultList = new List<PieChartModel>();
            foreach (KeyValuePair<Guid, int> entry in serviceDict)
            {
                resultList.Add(new PieChartModel() {
                    y = Math.Round(entry.Value / count * 100),
                    legendText = serviceNameDict[entry.Key],
                    label = serviceNameDict[entry.Key],
                });
            }
            string json = JsonConvert.SerializeObject(resultList);
            return json;
        }
    }

    public class PieChartModel
    {
        public double y { get; set; }
        public string legendText { get; set; }
        public string label { get; set; }
    }


}