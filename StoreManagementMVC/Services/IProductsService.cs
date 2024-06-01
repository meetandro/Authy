using StoreManagementMVC.Models;

namespace StoreManagementMVC.Services
{
    public interface IProductsService
    {
        List<Product> GetAllProducts();

        Product? GetProductById(int id);

        Product AddProduct(ProductDto productDto);

        Product EditProduct(int id, ProductDto productDto);

        Product DeleteProduct(int id);
    }
}
