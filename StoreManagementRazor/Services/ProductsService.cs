using StoreManagementRazor.Extensions;
using StoreManagementRazor.Models;
using StoreManagementRazor.Repositories;

namespace StoreManagementRazor.Services
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

            var product = productDto.ToProduct();
            product.ImageFileName = imageFileName;

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

            product = productDto.ToUpdatedProduct(product);
            product.ImageFileName = imageFileName;

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
