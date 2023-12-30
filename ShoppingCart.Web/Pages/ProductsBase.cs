using Microsoft.AspNetCore.Components;
using ShoppingCart.Models.Dtos;
using ShoppingCart.Web.Services.Contracts;

namespace ShoppingCart.Web.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public required IProductService ProductService { get; set; }

        public required IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetProducts();
        }
    }
}
