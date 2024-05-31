using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Models;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class CreateModel(IWebHostEnvironment environment, ApplicationDbContext context) : PageModel
    {
        private readonly IWebHostEnvironment _environment = environment;
        private readonly ApplicationDbContext _context = context;

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

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/products/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                ProductDto.ImageFile.CopyTo(stream);
            }

            var product = new Product()
            {
                Name = ProductDto.Name,
                Brand = ProductDto.Brand,
                Category = ProductDto.Category,
                Description = ProductDto.Description ?? "",
                ImageFileName = newFileName,
                Price = ProductDto.Price,
                CreatedAt = DateTime.Now,
            };

            context.Products.Add(product);
            context.SaveChanges();

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
