using Microsoft.AspNetCore.Components;
using ShoppingCart.Models.Dtos;
using ShoppingCart.Web.Services.Contracts;

namespace ShoppingCart.Web.Pages
{
    public class ProductDetailsBase : ComponentBase
    {

        [Inject]
        public required IProductService ProductService { get; set; }

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

    }
}
