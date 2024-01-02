using OnlineShopCart.API.Entities;
using OnlineShopCart.Models.Dtos;

namespace OnlineShopCart.API.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ToProductDtos(this IEnumerable<Product> products, IEnumerable<ProductCategory> productCategories)
        {
            return (from p in products
                    join c in productCategories on p.CategoryId equals c.Id
                    select new ProductDto
                    {
                        Id = p.Id,
                        ProductName = p.Name,
                        ProductDescription = p.Description,
                        ProductImageURL = p.ImageURL,
                        ProductPrice = p.Price,
                        Quantity = p.Quantity,
                        CategoryId = p.CategoryId,
                        CategoryName = c.Name
                    }).ToList();

            //IEnumerable<ProductDto> productDtos = products.Select(p => new ProductDto
            //{
            //    Id = p.Id,
            //    ProductName = p.Name,
            //    ProductDescription = p.Description,
            //    ProductImageURL = p.ImageURL,
            //    ProductPrice = p.Price,
            //    Quantity = p.Quantity,
            //    CategoryId = p.CategoryId,
            //    CategoryName = productCategories.FirstOrDefault(c => c.Id == p.CategoryId).Name
            //});

            //return productDtos;
        }

        public static ProductDto ToProductDto(this Product product, ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                ProductPrice = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name
            };
        }

        public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
        {
            return (from c in cartItems
                    join p in products on c.ProductId equals p.Id
                    select new CartItemDto
                    {
                        Id = c.Id,
                        ProductId = c.ProductId,
                        ProductName = p.Name,
                        ProductDescription = p.Description,
                        ProductImageURL = p.ImageURL,
                        ProductPrice = p.Price,
                        CartId = c.CartId,
                        Quantity = c.Quantity,
                        TotalPrice = c.Quantity * p.Price
                    }).ToList();

        }

        public static CartItemDto ConvertToDto(this CartItem cartItem, Product product)
        {
            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                ProductPrice = product.Price,
                CartId = cartItem.CartId,
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.Quantity * product.Price
            };
        }
    }
}
