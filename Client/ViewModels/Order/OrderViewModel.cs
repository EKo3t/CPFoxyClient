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
        public Guid Id { get; set; }

        [Required]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime OrderTime { get; set; }

        [Required]
        public string StartAddress { get; set; }

        public string EndAddress { get; set; }

        public OrderStatus Status { get; set; }

        public ServiceVM Service { get; set; }

        public Guid ServiceId { get; set; }

        public String Email { get; set; }

        public string OrderTimeString { get; set; }
    }
}