using System.ComponentModel.DataAnnotations;

namespace ProjectSmart.Models
{
    public class AdminUsers
    {

        [Key]
        public int AdminId { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminMiddleName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminContactNumber { get; set; }
        public DateTime AdminDateOfBirth { get; set; }
        public string AdminAddress1 { get; set; }
        //
        public string AdminAddress2 { get; set; }
        public string AdminCity { get; set; }
        public string AdminRegion { get; set; }
        public string AdminPosition { get; set; }
        public string AdminBranch { get; set; }

    }
}
