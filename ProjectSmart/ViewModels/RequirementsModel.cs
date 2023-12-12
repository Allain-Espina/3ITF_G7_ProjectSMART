using ProjectSmart.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FileExtensionsAttribute = System.ComponentModel.DataAnnotations.FileExtensionsAttribute;

namespace ProjectSmart.ViewModels
{
    public class RequirementsModel
    {
        [Required]
        public string? ScholarEmailAddress { get; set; }

        [Display(Name = "Current Term")]
        [Required(ErrorMessage = "Please select from the given choices.")]
        public Term Term { get; set; }

        public string? AcademicYear { get; set; }

        public string? FileName { get; set; }

        [Required(ErrorMessage = "Please upload a PDF File.")]
        [FileExtensions(Extensions = "pdf")]
        [NotMapped]
        public IFormFile? File { get; set; }

        public string? FilePath { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public string? DateUploaded { get; set; } = DateTime.Now.Date.ToString("MM/dd/yyyy");
    }
}
