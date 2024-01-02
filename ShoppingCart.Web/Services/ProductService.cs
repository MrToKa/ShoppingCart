using OnlineShopCart.Models.Dtos;
using OnlineShopCart.Web.Services.Contracts;
using System.Net.Http.Json;

namespace OnlineShopCart.Web.Services
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

        public async Task<ProductDto?> GetProduct(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"api/product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    ProductDto? product = await response.Content.ReadFromJsonAsync<ProductDto>();
                    return product;
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("api/product");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }

                    IEnumerable<ProductDto>? products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                    return products;
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public Task<ProductDto> UpdateProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
