using ShoppingCart.Models.Dtos;
using ShoppingCart.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShoppingCart.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<ProductDto> AddProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                IEnumerable<ProductDto>? products = await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/product");
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<ProductDto> UpdateProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
