using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.ViewModels
{
    public class ChangePWModel
    {

        [Display(Name = "User's Email Address:")]
        [Required(ErrorMessage = "Email Address is required.")]
        public string? Email { get; set; }

        [Display(Name = "User's Current Password:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Current Password is required.")]
        public string? CurrentPassword { get; set; }

        [Display(Name = "User's New Password:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string? NewPassword { get; set; }

        [Display(Name = "Confirm User's New Password:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Confirmation is required.")]
        public string? ConfirmNewPassword { get; set; }

    }
}
