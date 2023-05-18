using Capstone_23_Proteine.Data.Migrations;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AboutMe>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            builder.Entity<FoodIntake>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);
        }

        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<FoodIntake> FoodIntake { get; set; }
    }
}