using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.Models
{
    public enum Tag
    {
        Registration_Form, Certified_Grades, Terminal_Report, Gratitude_Letter, Others
    }
    public class Connect
    {
        [Key]
        public int ConnectId { get; set; }
        [Display(Name = "Scholar's Email Address")]
        [Required]
        public string ScholarEmailAddress { get; set; }
        public Tag ConnectTag { get; set; }
        [Required]
        [Display(Name = "Comment")]
        public string ConnectContent { get; set; }
    }
}