using System.ComponentModel.DataAnnotations;
namespace ProjectSmart.Models
{

    public class ScholarUsers
    {
        [Key]
        public int ScholarId { get; set; }
        public string ScholarFirstName { get; set; }
        public string ScholarMiddleName { get; set; }
        public string ScholarLastName { get; set; }
        public string ScholarEmail { get; set; }
        public string ScholarContactNumber { get; set; }
        public DateTime ScholarDateOfBirth { get; set; }
        public string ScholarUniversity { get; set; }
        //
        public string ScholarAddress1 { get; set; }
        //
        public string ScholarAddress2 { get; set; }
        public string ScholarCity { get; set; }
        public string ScholarRegion { get; set; }

    }
}
