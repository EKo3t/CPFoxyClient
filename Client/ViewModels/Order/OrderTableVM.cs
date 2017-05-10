using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Order
{
    public class OrderTableVM
    {
        public string UserEmail { get; set; }

        public List<OrderViewModel> Orders { get; set; }

    }
}