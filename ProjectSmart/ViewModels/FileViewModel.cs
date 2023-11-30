using ProjectSmart.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.ViewModels
{
    public class FileViewModel
    {
        public IEnumerable<Certified_Grades> TOR { get; set; }
        public IEnumerable<Registration_Form> RF { get; set; }
        public IEnumerable<Terminal_Report_Form> TRF { get; set; }
        public IEnumerable<Gratitude_Letter> GL { get; set; }

        /*public Transcript_of_Records tor { get; set; }
        public Registration_Form rf { get; set; }
        public Terminal_Report_Form trf { get; set; }
        public Gratitude_Letter gl { get; set; }*/


        /*[Display(Name = "Scholar's Email Address")]
        public string ScholarEmail { get; set; }
        [NotMapped]
        public IFormFile? TOR_Filename { get; set; }
        public string? TOR_Filepath { get; set; }
        public string TOR_DocuStatus { get; set; } = "FOR REVIEW";
        public DateTime? TOR_UploadDate { get; set; } = DateTime.Now.Date;

        public IFormFile? RF_Filename { get; set; }
        public string? RF_Filepath { get; set; }
        public string RF_DocuStatus { get; set; } = "FOR REVIEW";
        public DateTime? RF_UploadDate { get; set; } = DateTime.Now.Date;

        public IFormFile? TRF_Filename { get; set; }
        public string? TRF_Filepath { get; set; }
        public string TRF_DocuStatus { get; set; } = "FOR REVIEW";
        public DateTime? TRF_UploadDate { get; set; } = DateTime.Now.Date;

        public IFormFile? GL_Filename { get; set; }
        public string? GL_Filepath { get; set; }
        public string GL_DocuStatus { get; set; } = "FOR REVIEW";
        public DateTime? GL_UploadDate { get; set; } = DateTime.Now.Date;*/

    }
}
