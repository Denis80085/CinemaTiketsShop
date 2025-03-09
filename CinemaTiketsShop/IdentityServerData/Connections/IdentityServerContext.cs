using CinemaTiketsShop.IdentityServerData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.IdentityServerData.Connections
{
    public class IdentityServerContext : IdentityDbContext<User>
    {
        public IdentityServerContext(DbContextOptions<IdentityServerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("First Name must be at least 2 and maximum 30 characters long");
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("Last Name must be at least 2 and maximum 30 characters long");
                entity.Property(e => e.Titel)
                    .HasMaxLength(15)
                    .HasComment("Titel cant have more than 15 characters");
            });

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Name = "Costumer",
                    NormalizedName = "COSTUMER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
