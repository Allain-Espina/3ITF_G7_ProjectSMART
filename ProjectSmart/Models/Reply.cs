using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSmart.Models
{
    public class Reply
    {
        private DateTime _date = DateTime.Now;
        public Reply()
        {
            
        }
        [Key]
        public int ReplyId { get; set; }
        [Required]
        public string ReplyContent { get; set; }
        [Required]
        public string ScholarEmailAddress { get; set; }
        [Required]
        public int ConnectId { get; set; }
        public DateTime CurrentTime
        {
            get { return _date; }
            set { _date = value; }
        }
        public virtual Connect connect { get; set; }
        public Reply(int connectId)
        {
            this.ConnectId = connectId;
        }
    }
}
