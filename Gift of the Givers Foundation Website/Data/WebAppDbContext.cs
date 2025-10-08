using Gift_of_the_Givers_Foundation_Website.Models;
using Microsoft.EntityFrameworkCore;

namespace Gift_of_the_Givers_Foundation_Website.Data
{
    public class WebAppDbContext:DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Project> Projects { get; set; }
        public DbSet<Models.Donation> Donations { get; set; }
        public DbSet<Models.VolunteerSignUp> VolunteerSignUps { get; set; }
        public DbSet<Models.VolunteerProfile> VolunteerProfiles { get; set; }
        public DbSet<Models.DisasterAlert> DisasterAlerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1:1 User → VolunteerProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.VolunteerProfile)
                .WithOne(v => v.User)
                .HasForeignKey<VolunteerProfile>(v => v.UserID);

            // 1:N User → Projects
            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserID);

            // 1:N User → Donations
            modelBuilder.Entity<User>()
                .HasMany(u => u.Donations)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserID);

            // 1:N Project → Donations
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Donations)
                .WithOne(d => d.Project)
                .HasForeignKey(d => d.ProjectID);

            // 1:N Project → VolunteerSignUps
            modelBuilder.Entity<Project>()
                .HasMany(p => p.VolunteerSignUps)
                .WithOne(v => v.Project)
                .HasForeignKey(v => v.ProjectID);


        }
    }
}
