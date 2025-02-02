using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using UserProfiles_API.Models;
using UserProfiles_API.Helper;

namespace UserProfiles_API.Databases
{
    public class UserProfilesDbContext : DbContext
    {
        public DbSet<UserProfile> UserProfileData { get; set; }

        public UserProfilesDbContext(DbContextOptions<UserProfilesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .Property(u => u.DateOfBirth)
                .HasConversion(new Helper.DateOnlyConverter());

            base.OnModelCreating(modelBuilder);
        }
    }
}
