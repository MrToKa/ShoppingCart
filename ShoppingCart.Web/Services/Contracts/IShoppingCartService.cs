using OnlineShopCart.Models.Dtos;

namespace OnlineShopCart.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>>? GetItems(int userId);
        Task<CartItemDto?> AddItem(CartItemToAddDto item);
        Task<CartItemDto?> GetItem(int id);
        Task<CartItemDto?> UpdateItem(CartItemDto item);
        Task DeleteItem(int id);
    }
}
