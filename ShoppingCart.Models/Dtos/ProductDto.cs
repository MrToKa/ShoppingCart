namespace ShoppingCart.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public required string ProductImageURL { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
