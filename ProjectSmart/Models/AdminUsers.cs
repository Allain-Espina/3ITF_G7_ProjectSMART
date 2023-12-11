using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.Models
{
    public class AdminUsers
    {

        [Key]
        public int AdminId { get; set; }

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "Please provide the Administrator's First Name.")]
        public string AdminFirstName { get; set; }

        [Display(Name = "Middle Name:")]
        [Required(ErrorMessage = "Please provide the Administrator's Middle Name.")]
        public string AdminMiddleName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Please provide the Administrator's Last Name.")]
        public string AdminLastName { get; set; }

        [Display(Name = "Email Address:")]
        [Required(ErrorMessage = "Please provide the Administrator's Email Address.")]
        [DataType(DataType.EmailAddress)]
        public string AdminEmail { get; set; }

        [Display(Name = "Role:")]
        public string AdminRole { get; set; } = "Admin";

        [Display(Name = "Contact Number:")]
        [Required(ErrorMessage = "Please provide the Administrator's Contact Number.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0]{1}[9]{1}[0-9]{2}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Please follow this format: 09XX-XXX-XXXX")]
        public string AdminContactNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Please provide the Administrator's birth date.")]
        public string AdminDateOfBirth { get; set; }

        [Display(Name = "Address Line 1:")]
        [Required(ErrorMessage = "Please provide the Administrator's Home Address (Line 1).")]
        public string AdminAddress1 { get; set; }

        [Display(Name = "Address Line 2:")]
        [Required(ErrorMessage = "Please provide the Administrator's Home Address (Line 2).")]
        public string AdminAddress2 { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "Please provide the Administrator's City of residence.")]
        public string AdminCity { get; set; }

        [Display(Name = "Region:")]
        [Required(ErrorMessage = "Please select a region from where the Administrator lives.")]
        public string AdminRegion { get; set; }

        [Display(Name = "Administrator's Position:")]
        [Required(ErrorMessage = "Please select the Administrator's Position within the organization.")]
        public string AdminPosition { get; set; }

        [Display(Name = "DOST Regional Branch Office:")]
        [Required(ErrorMessage = "Please select a DOST Regional Branch Office where the Administrator is assigned.")]
        public string AdminBranch { get; set; }

    }
}
