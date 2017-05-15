using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Order
{
    public class OrderInfoVM
    {
        [Display(Name = "Время заказа: ")]
        public string OrderTime { get; set; }

        [Display(Name = "Название услуги: ")]
        public string ServiceName { get; set; }

        [Display(Name = "Водитель: ")]
        public string FullName { get; set; }

        [Display(Name = "Машина: ")]
        public string CarSpec { get; set; }
    }
}