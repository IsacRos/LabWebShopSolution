namespace LabWebShop.Models
{
    public class CartItemDto
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public required string ProductId { get; set; }
        public required int Quantity { get; set; }

    }
}
