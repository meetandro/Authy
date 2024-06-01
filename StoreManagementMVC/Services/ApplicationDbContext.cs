using Microsoft.EntityFrameworkCore;
using StoreManagementMVC.Models;

namespace StoreManagementMVC.Services
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
