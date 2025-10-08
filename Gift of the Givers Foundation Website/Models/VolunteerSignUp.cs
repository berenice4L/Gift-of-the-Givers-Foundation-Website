using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gift_of_the_Givers_Foundation_Website.Models
{
    public class VolunteerSignUp
    {
        [Key]
        public int SignUpID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        [DataType(DataType.Date)]
        public DateTime SignUpDate { get; set; } = DateTime.Now;

        // 🔗 Navigation Properties
        public User? User { get; set; }
        public Project? Project { get; set; }
    }
}
