﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopCart.API.Extensions;
using OnlineShopCart.API.Repositories.Contracts;
using OnlineShopCart.Models.Dtos;

namespace OnlineShopCart.API.Controllers
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                Entities.Product? product = await productRepository.GetProductByIdAsync(id);
                Entities.ProductCategory? productCategory = await productRepository.GetCategoryAsync(product.CategoryId);

                if (product == null || productCategory == null)
                {
                    return NotFound();
                }
                else
                {
                    ProductDto productDto = product.ToProductDto(productCategory);
                    return Ok(productDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
