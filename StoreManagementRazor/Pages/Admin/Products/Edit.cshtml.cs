using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Models;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class EditModel(IWebHostEnvironment environment, ApplicationDbContext context) : PageModel
    {
        private readonly IWebHostEnvironment _environment = environment;
        private readonly ApplicationDbContext _context = context;

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

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
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

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            string newFileName = product.ImageFileName;
            if (ProductDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(ProductDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductDto.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            product.Name = ProductDto.Name;
            product.Brand = ProductDto.Brand;
            product.Category = ProductDto.Category;
            product.Price = ProductDto.Price;
            product.Description = product.Description;
            product.ImageFileName = newFileName;

            context.SaveChanges();

            Product = product;

            successMessage = "Product edited successfully";
        }
    }
}
