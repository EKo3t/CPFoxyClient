using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Order
{
    public class OrderViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderTime { get; set; }

        [Required]
        public string StartAddress { get; set; }

        public string EndAddress { get; set; }

        public OrderStatus Status { get; set; }
    }
}