using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.ViewModel.Auth
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Please enter non empty current password")]
        [StringLength(100, ErrorMessage = "Too long current password", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Please enter non empty new password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /* [Required(ErrorMessage = "Please enter non empty email")]
        [StringLength(100, ErrorMessage = "Too long email", MinimumLength = 5)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }*/
    }
}
