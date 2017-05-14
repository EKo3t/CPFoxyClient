using Client.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Manager
{
    public class ManagerVM
    {
        public List<OrderViewModel> Orders { get; set; }

        public List<DriverVM> Drivers { get; set; }

        public List<CarVM> Cars { get; set; }
    }
}