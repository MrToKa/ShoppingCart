using OnlineShopCart.API.Entities;
using OnlineShopCart.Models.Dtos;

namespace OnlineShopCart.API.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem?> AddItem(CartItemToAddDto item);
        Task<CartItem?> UpdateQuantity(int id, CartItemQuantityUpdateDto item);
        Task<CartItem?> DeleteItem(int id);
        Task<CartItem?> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);
    }
}
