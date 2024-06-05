using Microsoft.AspNetCore.Mvc;
using StoreManagementMVC.Extensions;
using StoreManagementMVC.Models;
using StoreManagementMVC.Services;

namespace StoreManagementMVC.Controllers
{
    public class ProductsController(IProductsService productsService) : Controller
    {
        private readonly IProductsService _productsService = productsService;

        public IActionResult Index()
        {
            var products = _productsService.GetAllProducts();
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

            _productsService.AddProduct(productDto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = _productsService.GetProductById(id);
            if (product is null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["Id"] = product.Id;
            ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
            ViewData["ImageFileName"] = product.ImageFileName;

            var productDto = product.ToProductDto();
            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto)
        {
            var product = _productsService.GetProductById(id);
            if (product is null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                ViewData["Id"] = product.Id;
                ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
                ViewData["ImageFileName"] = product.ImageFileName;

                return View(productDto);
            }

            _productsService.EditProduct(id, productDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productsService.GetProductById(id);
            if (product is null)
            {
                return RedirectToAction(nameof(Index));
            }

            _productsService.DeleteProduct(product.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
