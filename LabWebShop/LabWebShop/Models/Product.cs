using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace LabWebShop.Models;

[Collection("products")]
public class Product
{
    public ObjectId Id { get; set; }
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public required double PriceEur { get; set; }
    public string ImgUri { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;

}
