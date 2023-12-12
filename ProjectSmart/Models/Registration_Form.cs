using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.Models
{
    public class Registration_Form
    {
        [Key]
        public int RF_ID { get; set; }

        [Required]
        public string? ScholarEmailAddress { get; set; }

        [Display(Name = "Current Term:")]
        [Required(ErrorMessage = "Please select from the given choices.")]
        public Term RF_Term { get; set; }

        public string? RF_AcademicYear { get; set; }

        public string? RF_FileName { get; set; }

        [FileExtensions(Extensions = "pdf", ErrorMessage = "Please upload your Registration Form in PDF Format.")]
        [Display(Name = "Registration Form:")]
        [NotMapped]
        public IFormFile? RF_File { get; set; }

        public string? RF_FilePath { get; set; }

        public string? RF_Status { get; set; }

        public string? RF_DateUploaded { get; set; } = DateTime.Now.Date.ToString("MM/dd/yyyy");
    }
}
