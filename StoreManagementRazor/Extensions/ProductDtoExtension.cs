using StoreManagementRazor.Models;

namespace StoreManagementRazor.Extensions
{
    public static class ProductDtoExtension
    {
        public static Product ToProduct(this ProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Description = productDto.Description ?? "",
                Price = productDto.Price,
                CreatedAt = DateTime.Now
            };
        }

        public static Product ToUpdatedProduct(this ProductDto productDto, Product product)
        {
            product.Name = productDto.Name;
            product.Brand = productDto.Brand;
            product.Category = productDto.Category;
            product.Description = productDto.Description ?? "";
            product.Price = productDto.Price;
            return product;
        }
    }
}
