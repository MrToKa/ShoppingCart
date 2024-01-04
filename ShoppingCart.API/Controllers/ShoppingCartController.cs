using Microsoft.AspNetCore.Mvc;
using OnlineShopCart.API.Entities;
using OnlineShopCart.API.Extensions;
using OnlineShopCart.API.Repositories.Contracts;
using OnlineShopCart.Models.Dtos;


namespace OnlineShopCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItem>> GetItem(int id)
        {
            try
            {
                CartItem? item = await shoppingCartRepository.GetItem(id);
                if (item == null)
                {
                    return NoContent();
                }

                Product product = await productRepository.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception("No product exists in the the database");
                }

                CartItemDto cartItemDto = item.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("{userId:int}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetItems(int userId)
        {
            try
            {
                IEnumerable<CartItem> cartItems = await shoppingCartRepository.GetItems(userId);
                if (cartItems == null)
                {
                    return NoContent();
                }
                else
                {
                    IEnumerable<Product> products = await productRepository.GetProductsAsync();
                    if (products == null)
                    {
                        throw new Exception("No products exist in the the database");
                    }

                    IEnumerable<CartItemDto> cartItemsDto = cartItems.ConvertToDto(products);

                    return Ok(cartItemsDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddItem([FromBody] CartItemToAddDto item)
        {
            try
            {
                CartItem? newCartItem = await shoppingCartRepository.AddItem(item);
                if (newCartItem == null)
                {
                    return NoContent();
                }

                Product product = await productRepository.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception("No product exists in the the database");
                }

                CartItemDto cartItemDto = newCartItem.ConvertToDto(product);

                return CreatedAtAction(nameof(GetItem), new { id = cartItemDto.Id }, cartItemDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
                CartItem? itemToDelete = await shoppingCartRepository.DeleteItem(id);
                if (itemToDelete == null)
                {
                    return NoContent();
                }

                Product product = await productRepository.GetProductByIdAsync(itemToDelete.ProductId);
                if (product == null)
                {
                    throw new Exception("No product exists in the the database");
                }

                CartItemDto cartItemDto = itemToDelete.ConvertToDto(product);

                return Ok(cartItemDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
