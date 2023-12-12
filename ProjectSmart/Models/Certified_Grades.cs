using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;

namespace ProjectSmart.Models
{
    public enum Term
    {
        First_Term, Second_Term, Mid_Year
    }

    public class Certified_Grades
    {
        [Key]
        public int CG_ID { get; set; }

        [Required]
        public string? ScholarEmailAddress { get; set; }

        [Display(Name = "Current Term")]
        [Required(ErrorMessage = "Please select from the given choices.")]
        public Term CG_Term { get; set; }

        public string? CG_AcademicYear { get; set; }

        public string? CG_FileName { get; set; }

        [FileExtensions(Extensions = "pdf")]
        [NotMapped]
        public IFormFile? CG_File { get; set; }

        public string? CG_FilePath { get; set; }

        [Required]
        public string? CG_Status { get; set; }

        [Required]
        public string? CG_DateUploaded { get; set; } = DateTime.Now.Date.ToString("MM/dd/yyyy");
    }
}
