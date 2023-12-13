using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace ProjectSmart.Models
{
    public enum Tag
    {
        Registration_Form, Certified_Grades, Terminal_Report, Gratitude_Letter, Others
    }
    public class Connect
    {
        private DateTime _date = DateTime.Now;

        [Key]
        public int ConnectId { get; set; }
        [Display(Name = "Scholar's Email Address")]
        [Required]
        public string ScholarEmailAddress { get; set; }
        public Tag ConnectTag { get; set; }
        [Required]
        [Display(Name = "Comment")]
        public string ConnectContent { get; set; }
        public DateTime CurrentTime
        {
            get { return _date; }
            set { _date = value; }
        }
        public virtual ICollection<Reply>? Replies { get; set; }

    }
}