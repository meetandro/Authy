using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Models;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class IndexModel(IProductsService productsService) : PageModel
    {
        private readonly IProductsService _productsService = productsService;

        public List<Product> Products { get; set; } = [];

        public void OnGet()
        {
            Products = _productsService.GetAllProducts();
        }
    }
}
