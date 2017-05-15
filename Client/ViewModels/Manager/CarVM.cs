using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Manager
{
    public class CarVM
    {        
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Марка")]
        public string CarMark { get; set; }

        [Required]
        [Display(Name = "Модель")]
        public string CarModel { get; set; }

        [Required]
        [Display(Name = "Цвет")]
        public string CarColor { get; set; }
    }
}