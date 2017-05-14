using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter non empty email")]
        [StringLength(100, ErrorMessage = "Too long email", MinimumLength = 5)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter non empty password")]
        [StringLength(100, ErrorMessage = "Too short password", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter non empty email")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Too short password's confirm", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public string BirthDate { get; set; }
    }
}