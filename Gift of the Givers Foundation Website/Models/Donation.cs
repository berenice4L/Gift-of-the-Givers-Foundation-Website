using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gift_of_the_Givers_Foundation_Website.Models
{
    public class Donation
    {
        [Key]
        public int DonationID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime DonationDate { get; set; } = DateTime.Now;

        public string? PaymentStatus { get; set; }

        // 🔗 Navigation Properties
        public User? User { get; set; }
        public Project? Project { get; set; }
    }
}
