using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.ViewModels
{
    public class ScholarLoginModel
    {

        [Display(Name = "Scholar's Email Address:")]
        [Required(ErrorMessage = "Email Address is required.")]
        public string? Email { get; set; }

        [Display(Name = "Scholar's Password:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

    }
}
