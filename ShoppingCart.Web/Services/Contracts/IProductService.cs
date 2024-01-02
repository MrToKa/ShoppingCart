using OnlineShopCart.Models.Dtos;

namespace OnlineShopCart.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto?> GetProduct(int id);
        Task<ProductDto> AddProduct(ProductDto product);
        Task<ProductDto> UpdateProduct(ProductDto product);
        Task DeleteProduct(int id);
    }
}
