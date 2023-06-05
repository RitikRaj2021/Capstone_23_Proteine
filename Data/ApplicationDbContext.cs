using Capstone_23_Proteine.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone_23_Proteine.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Override the OnModelCreating method to configure entity relationships
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the one-to-many relationship between AboutMe and User
            builder.Entity<AboutMe>()
                .HasOne(a => a.User) // AboutMe entity has one User
                .WithMany() // User entity can have multiple AboutMe entries
                .HasForeignKey(a => a.UserId); // Foreign key is UserId

            // Configure the one-to-many relationship between SetGoals and User
            builder.Entity<SetGoals>()
                .HasOne(u => u.User) // SetGoals entity has one User
                .WithMany() // User entity can have multiple SetGoals entries
                .HasForeignKey(u => u.UserId); // Foreign key is UserId

            // Configure the one-to-many relationship between FoodIntake and User
            builder.Entity<FoodIntake>()
                .HasOne(f => f.User) // FoodIntake entity has one User
                .WithMany() // User entity can have multiple FoodIntake entries
                .HasForeignKey(f => f.UserId); // Foreign key is UserId

            
        }



        // DbSet for the SetGoals entity
        public DbSet<SetGoals> SetGoals { get; set; }

        // DbSet for the AboutMe entity
        public DbSet<AboutMe> AboutMe { get; set; }

        // DbSet for the FoodIntake entity
        public DbSet<FoodIntake> FoodIntake { get; set; }


    }
}
