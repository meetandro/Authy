using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Models;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class IndexModel(ApplicationDbContext context) : PageModel
    {
        private readonly ApplicationDbContext _context = context;

        public void OnGet()
        {
            Products = _context.Products
                .OrderByDescending(p => p.Id)
                .ToList();
        }

        public List<Product> Products { get; set; } = [];
    }
}
