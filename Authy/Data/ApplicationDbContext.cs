using Authy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authy.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var administrator = new IdentityRole("Administrator")
            {
                NormalizedName = "administrator"
            };

            var client = new IdentityRole("Client")
            {
                NormalizedName = "client"
            };

            var seller = new IdentityRole("Seller")
            {
                NormalizedName = "seller"
            };

            builder.Entity<IdentityRole>().HasData(administrator, client, seller);
        }
    }
}
