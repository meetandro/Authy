using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Models;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class EditModel(IProductsService productsService) : PageModel
    {
        private readonly IProductsService _productsService = productsService;

        [BindProperty]
        public ProductDto ProductDto { get; set; } = new();

        public Product Product { get; set; } = new();

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet(int? id)
        {
            if (id is null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            var product = _productsService.GetProductById((int)id);
            if (product is null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            ProductDto.Name = product.Name;
            ProductDto.Brand = product.Brand;
            ProductDto.Category = product.Category;
            ProductDto.Description = product.Description;
            ProductDto.Price = product.Price;

            Product = product;
        }

        public void OnPost(int? id)
        {
            if (id is null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Provide all the required fields.";
                return;
            }

            var product = _productsService.GetProductById((int)id);
            if (product is null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            _productsService.EditProduct((int)id, ProductDto);

            Product = product;

            successMessage = "Product edited successfully";
        }
    }
}
