using StoreManagementMVC.Models;
using StoreManagementMVC.Services;

namespace StoreManagementMVC.Repositories
{
    public class ProductsRepository(ApplicationDbContext context) : IProductsRepository
    {
        private readonly ApplicationDbContext _context = context;

        public List<Product> GetAllProducts()
        {
            var products = _context.Products
                .OrderByDescending(p => p.Id)
                .ToList();
            return products;
        }

        public Product? GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product EditProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            var updatedProduct = GetProductById(product.Id);
            return updatedProduct;
        }

        public Product DeleteProduct(int id)
        {
            var product = GetProductById(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }
    }
}
