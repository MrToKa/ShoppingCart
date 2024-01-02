using Microsoft.AspNetCore.Components;
using OnlineShopCart.Models.Dtos;
using OnlineShopCart.Web.Services.Contracts;

namespace OnlineShopCart.Web.Pages
{
    public class ProductDetailsBase : ComponentBase
    {

        [Inject]
        public required IProductService ProductService { get; set; }

        [Inject]
        public required IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public required int Id { get; set; }

        public required ProductDto Product { get; set; }

        public required string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetProduct(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task AddToCart_ClickAsync(CartItemToAddDto itemToAddDto)
        {
            try
            {
                CartItemDto? cartItem = await ShoppingCartService.AddItem(itemToAddDto);
                NavigationManager.NavigateTo($"/ShoppingCart");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }
}
