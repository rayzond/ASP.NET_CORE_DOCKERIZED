using Microsoft.EntityFrameworkCore;
using AirfarePriceAlertSystem.Models;

namespace AirfarePriceAlertSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAlert> UserAlerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.UID)
                .ValueGeneratedOnAdd(); // Auto-increment User ID

            modelBuilder.Entity<UserAlert>()
                .Property(ua => ua.ID)
                .ValueGeneratedOnAdd(); // Auto-increment UserAlert ID
        }
    }
}
