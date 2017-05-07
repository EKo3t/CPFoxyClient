using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModel.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter non empty email")]
        [StringLength(100, ErrorMessage = "Too long email", MinimumLength = 5)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter non empty password")]
        [StringLength(100, ErrorMessage = "Too short password", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}