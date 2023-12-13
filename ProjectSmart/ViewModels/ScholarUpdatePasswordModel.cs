using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.ViewModels
{
    public class ScholarUpdatePasswordModel
    {
        [Display(Name = "Current Password:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please provide the current password.")]
        public string? CurrentPassword { get; set; }

        [Display(Name = "New Password:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please provide a new password.")]
        public string? NewPassword { get; set; }

        [Display(Name = "Confirm New Password:")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmNewPassword { get; set; }
    }
}
