﻿using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.ViewModels
{
    public class AdminLoginModel
    {

        [Display(Name = "Administrator's Email Address:")]
        [Required(ErrorMessage = "Email Address is required.")]
        public string? Email { get; set; }

        [Display(Name = "Administrator's Password:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }

    }
}
