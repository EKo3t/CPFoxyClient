using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите непустую почту")]
        [StringLength(100, ErrorMessage = "Слишком длинная почта", MinimumLength = 5)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите непустой пароль")]
        [StringLength(100, ErrorMessage = "Слишком короткий пароль", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите непустое подтверждение пароля")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Подтверждение пароля слишком короткое", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime BirthDate { get; set; }
    }
}