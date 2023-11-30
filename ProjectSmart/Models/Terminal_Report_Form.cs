using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.Models
{
    public class Terminal_Report_Form
    {
        [Key]
        public int TRF_ID { get; set; }
        public string ScholarEmailAddress { get; set; }
        [NotMapped]
        public IFormFile? TRF_FileName { get; set; }
        public string? TRF_FilePath { get; set; }
        public string TRF_Status { get; set; }
        public DateTime? TRF_DateUploaded { get; set; }
    }
}
