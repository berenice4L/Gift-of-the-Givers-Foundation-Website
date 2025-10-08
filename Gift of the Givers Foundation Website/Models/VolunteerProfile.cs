using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gift_of_the_Givers_Foundation_Website.Models
{
    public class VolunteerProfile
    {
        [Key]
        public int ProfileID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public string? Skills { get; set; }

        public string? Availability { get; set; }

        // 🔗 Navigation Property
        public User? User { get; set; }
    }
}
