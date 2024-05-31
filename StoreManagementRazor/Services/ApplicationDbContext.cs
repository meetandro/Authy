using Microsoft.EntityFrameworkCore;
using StoreManagementRazor.Models;

namespace StoreManagementRazor.Services
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
