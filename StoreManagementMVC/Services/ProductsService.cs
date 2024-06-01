using StoreManagementMVC.Models;
using StoreManagementMVC.Repositories;

namespace StoreManagementMVC.Services
{
    public class ProductsService(IProductsRepository productsRepository, IFileService fileService) : IProductsService
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IFileService _fileService = fileService;

        public List<Product> GetAllProducts()
        {
            return _productsRepository.GetAllProducts();
        }

        public Product? GetProductById(int id)
        {
            return _productsRepository.GetProductById(id);
        }

        public Product AddProduct(ProductDto productDto)
        {
            string imageFileName = _fileService.SaveFileInFolder(productDto.ImageFile, "products");

            var product = new Product()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Description = productDto.Description,
                ImageFileName = imageFileName,
                Price = productDto.Price,
                CreatedAt = DateTime.Now,
            };
            _productsRepository.AddProduct(product);
            return product;
        }

        public Product EditProduct(int id, ProductDto productDto)
        {
            var product = _productsRepository.GetProductById(id);
            string imageFileName = product.ImageFileName;

            if (productDto.ImageFile is not null)
            {
                imageFileName = _fileService.SaveFileInFolder(productDto.ImageFile, "products");

                _fileService.DeleteFileInFolder(product.ImageFileName, "products");
            }

            product.Name = productDto.Name;
            product.Brand = productDto.Brand;
            product.Category = productDto.Category;
            product.Description = productDto.Description;
            product.ImageFileName = imageFileName;
            product.Price = productDto.Price;

            _productsRepository.EditProduct(product);
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var product = _productsRepository.GetProductById(id);
            string imageFileName = product.ImageFileName;

            _fileService.DeleteFileInFolder(imageFileName, "products");

            return _productsRepository.DeleteProduct(product.Id);
        }
    }
}
