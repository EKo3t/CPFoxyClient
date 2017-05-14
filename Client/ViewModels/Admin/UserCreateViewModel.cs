using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Admin
{
    public class UserCreateViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "Слишком длинная почта", MinimumLength = 5)]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите непустой пароль")]
        [StringLength(100, ErrorMessage = "Слишком короткий пароль", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime BirthDate { get; set; }
    }
}