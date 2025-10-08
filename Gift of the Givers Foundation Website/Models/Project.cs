using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gift_of_the_Givers_Foundation_Website.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string? RequiredSkills { get; set; }

        public int? MaxVolunteers { get; set; }

        // 🔗 Navigation Properties
        public User? User { get; set; }
        public ICollection<Donation>? Donations { get; set; }
        public ICollection<VolunteerSignUp>? VolunteerSignUps { get; set; }
    }
}
