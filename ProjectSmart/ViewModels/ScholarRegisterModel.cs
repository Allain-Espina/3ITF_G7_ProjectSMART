using ProjectSmart.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.ViewModels
{
    public class ScholarRegisterModel
    {
        [Display(Name = "Email Address:")]
        [Required(ErrorMessage = "Please provide the Scholar's Email Address.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Role:")]
        public string? Role { get; set; } = "Scholar";

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "Please provide the Scholar's First Name.")]
        public string? FirstName { get; set; }

        [Display(Name = "Middle Name:")]
        [Required(ErrorMessage = "Please provide the Scholar's Middle Name.")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Please provide the Scholar's Last Name.")]
        public string? LastName { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Contact Number:")]
        [Required(ErrorMessage = "Please provide the Scholar's Contact Number.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0]{1}[9]{1}[0-9]{2}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Please follow this format: 09XX-XXX-XXXX")]
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Please provide the Scholar's birth date.")]
        public DateTime DateOfBirth {  get; set; }

        [Display(Name = "University:")]
        [Required(ErrorMessage = "Please indicate the University the Scholar is currently enrolled in.")]
        public string? University {  get; set; }

        [Display(Name = "Address Line 1:")]
        [Required(ErrorMessage = "Please provide the Scholar's Home Address (Line 1).")]
        public string? Address1 { get; set; }

        [Display(Name = "Address Line 2:")]
        [Required(ErrorMessage = "Please provide the Scholar's Home Address (Line 2).")]
        public string? Address2 { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "Please provide the Scholar's City of residence.")]
        public string? City { get; set; }

        [Display(Name = "Region:")]
        [Required(ErrorMessage = "Please select a region from where the Scholar lives.")]
        public string? Region { get; set; }

    }
}
