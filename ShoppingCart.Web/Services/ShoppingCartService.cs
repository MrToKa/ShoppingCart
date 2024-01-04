using OnlineShopCart.Models.Dtos;
using OnlineShopCart.Web.Services.Contracts;
using System.Net.Http.Json;

namespace OnlineShopCart.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CartItemDto?> AddItem(CartItemToAddDto item)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/ShoppingCart", item);

                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent
                        ? default
                        : await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Response was {response.StatusCode} and message: {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<CartItemDto?> DeleteItem(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"api/ShoppingCart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent
                     ? default
                     : await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Response was {response.StatusCode} and message: {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<CartItemDto?> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CartItemDto>>? GetItems(int userId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");

                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode == System.Net.HttpStatusCode.NoContent
                        ? Enumerable.Empty<CartItemDto>().ToList()
                        : await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<CartItemDto?> UpdateItem(CartItemDto item)
        {
            throw new NotImplementedException();
        }
    }
}
