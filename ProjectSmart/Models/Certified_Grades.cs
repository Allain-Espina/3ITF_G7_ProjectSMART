using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int TOR_ID { get; set; }
        [Display(Name = "Scholar's Email Address")]
        [Required]
        public string ScholarEmailAddress { get; set; }
        public Term Term { get; set; }
        public string AcademicYear { get; set; }
        [Required, FileExtensions(Extensions ="pdf")]
        [NotMapped]
        public IFormFile? CG_File { get; set; }
        public string? CG_FilePath { get; set; }
        [Required]
        public string CG_Status { get; set; }
        [Required]
        public string? CG_DateUploaded { get; set; }
    }
}
