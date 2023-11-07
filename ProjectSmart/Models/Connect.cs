namespace ProjectSmart.Models
{
    public enum Tag
    {
        Registration_Form, Transcript_of_Records, Terminal_Report, Gratitude_Letter, Others
    }
    public class Connect
    {
        public string ConnectId { get; set; }
        public string StudentID { get; set; }
        public string ConnectSubject { get; set; }
        public Tag ConnectTag { get; set; }
        public string ConnectContent { get; set; }
    }
}
