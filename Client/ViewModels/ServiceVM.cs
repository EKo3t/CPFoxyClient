using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels
{
    public class ServiceVM
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название услуги")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Начальная цена")]
        public double StartPrice { get; set; }

        [Required]
        [Display(Name = "Цена за километр")]
        [DataType(DataType.Currency)]
        public double PriceForKilometer { get; set; }
    }
}