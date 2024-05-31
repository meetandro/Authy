using Microsoft.AspNetCore.Mvc;
using StoreManagementMVC.Models;
using StoreManagementMVC.Services;

namespace StoreManagementMVC.Controllers
{
    public class ProductsController(ApplicationDbContext context, IWebHostEnvironment environment) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IWebHostEnvironment _environment = environment;

        public IActionResult Index()
        {
            var products = _context.Products
                .OrderByDescending(p => p.Id)
                .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (productDto.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDto.ImageFile!.FileName);

            string imageFullPath = $"{_environment.WebRootPath}/products/{newFileName}";
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDto.ImageFile.CopyTo(stream);
            }

            var product = new Product()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Description = productDto.Description,
                ImageFileName = newFileName,
                Price = productDto.Price,
                CreatedAt = DateTime.Now,
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return RedirectToAction("Index");
            }

            var productDto = new ProductDto()
            {
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Description = product.Description,
                Price = product.Price,
            };

            ViewData["Id"] = product.Id;
            ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
            ViewData["ImageFileName"] = product.ImageFileName;

            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                ViewData["Id"] = product.Id;
                ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
                ViewData["ImageFileName"] = product.ImageFileName;

                return View(productDto);
            }

            string newFileName = product.ImageFileName;

            if (productDto.ImageFile is not null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productDto.ImageFile.FileName);

                string imageFullPath = $"{_environment.WebRootPath}/products/{newFileName}";
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDto.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = $"{_environment.WebRootPath}/products/{product.ImageFileName}";
                System.IO.File.Delete(oldImageFullPath);
            }

            product.Name = productDto.Name;
            product.Brand = productDto.Brand;
            product.Category = productDto.Category;
            product.Description = productDto.Description;
            product.ImageFileName = newFileName;
            product.Price = productDto.Price;

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return RedirectToAction("Index");
            }

            string imageFullPath = $"{_environment.WebRootPath}/products/{product.ImageFileName}";
            System.IO.File.Delete(imageFullPath);

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
