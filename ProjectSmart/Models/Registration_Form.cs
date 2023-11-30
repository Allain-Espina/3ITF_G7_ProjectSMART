using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.Models
{
    public class Registration_Form
    {
        [Key]
        public int RF_ID { get; set; }
        public string ScholarEmailAddress { get; set; }
        [NotMapped]
        public IFormFile? RF_FileName { get; set; }
        public string? RF_FilePath { get; set; }
        public string RF_Status { get; set; }
        public DateTime? RF_DateUploaded { get; set; }
    }
}
