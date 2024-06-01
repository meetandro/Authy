﻿using Microsoft.AspNetCore.Mvc;
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

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = _productsService.GetProductById(id);
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
            var product = _productsService.GetProductById(id);
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

            _productsService.EditProduct(id, productDto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productsService.GetProductById(id);
            if (product is null)
            {
                return RedirectToAction("Index");
            }

            _productsService.DeleteProduct(product.Id);

            return RedirectToAction("Index");
        }
    }
}
