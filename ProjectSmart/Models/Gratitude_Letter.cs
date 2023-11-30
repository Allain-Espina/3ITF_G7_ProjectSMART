using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSmart.Models
{
    public class Gratitude_Letter
    {
        [Key]
        public int GL_ID { get; set; }
        public string ScholarEmailAddress { get; set; }
        [NotMapped]
        public IFormFile? GL_FileName { get; set; }
        public string? GL_FilePath { get; set; }
        public string GL_Status { get; set; }
        public DateTime? GL_DateUploaded { get; set; }
    }
}
