using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Manager
{
    public class DriverVM
    {
        public UserDetails UserDetails { get; set; }

        public CarVM Car { get; set; }

        public string CarID { get; set; }
    }
}