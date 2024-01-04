using Microsoft.AspNetCore.Components;
using OnlineShopCart.Models.Dtos;
using OnlineShopCart.Web.Services.Contracts;

namespace OnlineShopCart.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public required IShoppingCartService ShoppingCartService { get; set; }

        public required List<CartItemDto> CartItems { get; set; }

        public required string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteCartItem_Click(int cartItemId)
        {
            _ = await ShoppingCartService.DeleteItem(cartItemId);
            RemoveCartItem(cartItemId);
        }

        private CartItemDto? GetCartItem(int id)
        {
            return CartItems.FirstOrDefault(x => x.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            CartItemDto cartItem = GetCartItem(id);
            if (cartItem != null)
            {
                cartItem.Quantity--;
                if (cartItem.Quantity == 0)
                {
                    _ = CartItems.Remove(cartItem);
                }
            }
        }
    }
}
