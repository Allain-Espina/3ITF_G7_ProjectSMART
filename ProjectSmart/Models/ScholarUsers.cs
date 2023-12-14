using System.ComponentModel.DataAnnotations;
namespace ProjectSmart.Models
{

    public class ScholarUsers
    {
        [Key]
        public int ScholarId { get; set; }

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "Please provide the Scholar's First Name.")]
        public string ScholarFirstName { get; set; }

        [Display(Name = "Middle Name:")]
        [Required(ErrorMessage = "Please provide the Scholar's Middle Name.")]
        public string ScholarMiddleName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Please provide the Scholar's Last Name.")]
        public string ScholarLastName { get; set; }

        [Display(Name = "Email Address:")]
        [Required(ErrorMessage = "Please provide the Scholar's Email Address.")]
        [DataType(DataType.EmailAddress)]
        public string ScholarEmail { get; set; }

        [Display(Name = "Role:")]
        public string ScholarRole { get; set; } = "Scholar";

        [Display(Name = "Contact Number:")]
        [Required(ErrorMessage = "Please provide the Scholar's Contact Number.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0]{1}[9]{1}[0-9]{2}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Please follow this format: 09XX-XXX-XXXX")]
        public string ScholarContactNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        //[Required(ErrorMessage = "Please provide the Scholar's Date of Birth.")]
        public string ScholarDateOfBirth { get; set; }

        [Display(Name = "University:")]
        [Required(ErrorMessage = "Please indicate the University the Scholar is currently enrolled in.")]
        public string ScholarUniversity { get; set; }

        [Display(Name = "Address Line 1:")]
        [Required(ErrorMessage = "Please provide the Scholar's Home Address (Line 1).")]
        public string ScholarAddress1 { get; set; }

        [Display(Name = "Address Line 2:")]
        [Required(ErrorMessage = "Please provide the Scholar's Home Address (Line 2).")]
        public string ScholarAddress2 { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "Please provide the Scholar's City of residence.")]
        public string ScholarCity { get; set; }

        [Display(Name = "Region:")]
        [Required(ErrorMessage = "Please select a region from where the Scholar lives.")]
        public string ScholarRegion { get; set; }

    }
}
