using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Models;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class CreateModel(IProductsService productsService) : PageModel
    {
        private readonly IProductsService _productsService = productsService;

        [BindProperty]
        public ProductDto ProductDto { get; set; } = new();

        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            if (ProductDto.ImageFile is null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "Image is required");
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Provide all the required fields.";
                return;
            }

            _productsService.AddProduct(ProductDto);

            ProductDto.Name = "";
            ProductDto.Brand = "";
            ProductDto.Category = "";
            ProductDto.Price = 0;
            ProductDto.Description = "";
            ProductDto.ImageFile = null;

            ModelState.Clear();

            successMessage = "Product created successfully";
        }
    }
}
