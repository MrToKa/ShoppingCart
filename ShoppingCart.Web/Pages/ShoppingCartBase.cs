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
    }
}
