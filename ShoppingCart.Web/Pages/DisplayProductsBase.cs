using Microsoft.AspNetCore.Components;
using OnlineShopCart.Models.Dtos;

namespace OnlineShopCart.Web.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        [Parameter]
        public required IEnumerable<ProductDto> Products { get; set; }

    }
}
