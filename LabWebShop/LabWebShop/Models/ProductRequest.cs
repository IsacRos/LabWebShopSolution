namespace LabWebShop.Models
{
    public class ProductRequest
    {
        public required string ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public required double PriceEur { get; set; }
        public string ImgUri { get; set; } = string.Empty;
    }
}
