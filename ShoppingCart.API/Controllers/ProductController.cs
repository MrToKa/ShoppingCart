using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Extensions;
using ShoppingCart.API.Repositories.Contracts;
using ShoppingCart.Models.Dtos;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                IEnumerable<Entities.Product> products = await productRepository.GetProductsAsync();
                IEnumerable<Entities.ProductCategory> productCategories = await productRepository.GetCategoriesAsync();

                if (products == null || productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    IEnumerable<ProductDto> productDtos = products.ToProductDtos(productCategories);
                    return Ok(productDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
