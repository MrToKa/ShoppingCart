using ShoppingCart.API.Entities;

namespace ShoppingCart.API.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
        Task<ProductCategory> GetCategoryAsync(int id);
    }
}
