using ShoppingCart.Models.Dtos;

namespace ShoppingCart.API.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ToProductDtos(this IEnumerable<Entities.Product> products, IEnumerable<Entities.ProductCategory> productCategories)
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
    }
}
