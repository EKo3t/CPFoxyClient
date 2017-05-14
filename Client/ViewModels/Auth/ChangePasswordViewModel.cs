using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Auth
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Введите непустой пароль")]
        [StringLength(100, ErrorMessage = "Слишком короткий пароль", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Введите непустой пароль")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("NewPassword", ErrorMessage = "Новые пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        /* [Required(ErrorMessage = "Please enter non empty email")]
        [StringLength(100, ErrorMessage = "Too long email", MinimumLength = 5)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }*/
    }
}
