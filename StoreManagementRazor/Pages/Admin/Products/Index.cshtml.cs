using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Models;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class IndexModel(IProductsService productsService) : PageModel
    {
        private readonly IProductsService _productsService = productsService;

        public void OnGet()
        {
            Products = _productsService.GetAllProducts();
        }

        public List<Product> Products { get; set; } = [];
    }
}
