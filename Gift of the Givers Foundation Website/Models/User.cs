using System.ComponentModel.DataAnnotations;

namespace Gift_of_the_Givers_Foundation_Website.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? Location { get; set; }

        public DateTime DateRegistered { get; set; } = DateTime.Now;

        // 🔗 Navigation Properties
        public ICollection<Project>? Projects { get; set; }
        public ICollection<Donation>? Donations { get; set; }
        public ICollection<VolunteerSignUp>? VolunteerSignUps { get; set; }
        public VolunteerProfile? VolunteerProfile { get; set; }
    }
}
