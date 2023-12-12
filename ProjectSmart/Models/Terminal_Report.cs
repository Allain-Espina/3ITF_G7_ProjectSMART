using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.Models
{
    public class Terminal_Report
    {
        [Key]
        public int TR_ID { get; set; }

        [Required]
        public string? ScholarEmailAddress { get; set; }

        [Display(Name = "Current Term:")]
        [Required(ErrorMessage = "Please select from the given choices.")]
        public Term TR_Term { get; set; }

        public string? TR_AcademicYear { get; set; }

        public string? TR_FileName { get; set; }

        [FileExtensions(Extensions = "pdf", ErrorMessage = "Please upload your Terminal Report Form in PDF Format.")]
        [Display(Name = "Terminal Report Form:")]
        [NotMapped]
        public IFormFile? TR_File { get; set; }

        public string? TR_FilePath { get; set; }

        public string? TR_Status { get; set; }

        public string? TR_DateUploaded { get; set; } = DateTime.Now.Date.ToString("MM/dd/yyyy");
    }
}
