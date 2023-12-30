using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Data;
using ShoppingCart.API.Entities;

namespace ShoppingCart.API.Repositories.Contracts
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDBContext context;

        public ProductRepository(ShopOnlineDBContext context)
        {
            this.context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            bool productExists = context.Products.Any(p => p.Name == product.Name);
            if (productExists)
            {
                return await Task.FromResult<Product>(null);
            }

            Product productCreated = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageURL = product.ImageURL,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
            };

            _ = context.Products.Add(productCreated);
            _ = await context.SaveChangesAsync();
            return await Task.FromResult(productCreated);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            Product? product = context.Products.Find(id);
            if (product == null)
            {
                return await Task.FromResult(false);
            }

            _ = context.Products.Remove(product);
            _ = context.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync()
        {
            return await context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryAsync(int id)
        {
            ProductCategory? category = await context.ProductCategories.FindAsync(id);
            return category ?? await Task.FromResult<ProductCategory>(null);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product? product = await context.Products.FindAsync(id);
            return product ?? await Task.FromResult<Product>(null);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            Product? productToUpdate = context.Products.Find(product.Id);
            if (productToUpdate == null)
            {
                return await Task.FromResult<Product>(null);
            }

            Product productUpdated = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageURL = product.ImageURL,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
            };

            context.Entry(productToUpdate).CurrentValues.SetValues(productUpdated);
            _ = context.SaveChanges();
            return await Task.FromResult(productUpdated);
        }
    }
}
