using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSmart.Models
{
    public class Gratitude_Letter
    {
        [Key]
        public int GL_ID { get; set; }

        [Required]
        public string? ScholarEmailAddress { get; set; }

        [Display(Name = "Current Term")]
        [Required(ErrorMessage = "Please select from the given choices.")]
        public Term GL_Term { get; set; }

        public string? GL_AcademicYear { get; set; }

        public string? GL_FileName { get; set; }

        [FileExtensions(Extensions = "pdf")]
        [NotMapped]
        public IFormFile? GL_File { get; set; }

        public string? GL_FilePath { get; set; }

        [Required]
        public string? GL_Status { get; set; }

        [Required]
        public string? GL_DateUploaded { get; set; } = DateTime.Now.Date.ToString("MM/dd/yyyy");
    }
}
