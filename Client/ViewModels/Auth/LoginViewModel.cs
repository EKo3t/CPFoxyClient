using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите непустую почту")]
        [StringLength(100, ErrorMessage = "Слишком длинная почта", MinimumLength = 5)]
        [Display(Name = "Почта")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите непустой пароль")]
        [StringLength(100, ErrorMessage = "Слишком короткий пароль", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}