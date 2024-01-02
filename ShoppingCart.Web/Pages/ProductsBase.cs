using Microsoft.AspNetCore.Components;
using OnlineShopCart.Models.Dtos;
using OnlineShopCart.Web.Services.Contracts;

namespace OnlineShopCart.Web.Pages
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
