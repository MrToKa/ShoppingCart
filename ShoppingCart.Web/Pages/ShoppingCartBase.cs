using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                CartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CalculateTotalPrice();
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

            CalculateTotalPrice();
        }
        protected async Task UpdateQtyCartItem_Click(int id, int quantity)
        {
            try
            {
                if (quantity > 0)
                {
                    CartItemDto cartItem = GetCartItem(id);
                    if (cartItem != null)
                    {
                        cartItem.Quantity = quantity;
                        _ = await ShoppingCartService.UpdateItem(cartItem);
                    }

                    UpdateItemTotalPrice(cartItem);
                    CalculateTotalPrice();
                }
                else
                {
                    var cartItem = CartItems.FirstOrDefault(x => x.Id == id);
                    if (cartItem != null)
                    {
                        cartItem.Quantity = 1;
                        cartItem.TotalPrice = cartItem.ProductPrice;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

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
                //cartItem.Quantity--;
                //if (cartItem.Quantity == 0)
                //{
                _ = CartItems.Remove(cartItem);
                //}
            }
        }
        private void SetTotalPrice()
        {
            TotalPrice = CartItems.Sum(x => x.TotalPrice).ToString("C2");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = CartItems.Sum(x => x.Quantity);
        }
        private void CalculateTotalPrice()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }
        private void UpdateItemTotalPrice(CartItemDto cartItem)
        {
            var item = GetCartItem(cartItem.Id);

            if (item != null)
            {
                item.TotalPrice = cartItem.ProductPrice * cartItem.Quantity;
            }
        }

    }
}
