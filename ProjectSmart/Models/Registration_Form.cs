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

        [Required(ErrorMessage = "Please upload a PDF File.")]
        [FileExtensions(Extensions = "pdf")]
        [Display(Name = "Registration Form:")]
        [NotMapped]
        public IFormFile? RF_File { get; set; }

        public string? RF_FilePath { get; set; }

        [Required]
        public string? RF_Status { get; set; }

        [Required]
        public string? RF_DateUploaded { get; set; } = DateTime.Now.Date.ToString("MM/dd/yyyy");
    }
}
