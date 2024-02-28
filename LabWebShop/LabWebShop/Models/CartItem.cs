using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LabWebShop.Models
{
    [Collection("Cart")]
    public class CartItem
    {
        public ObjectId Id { get; set; }
        public required string UserId { get; set; }
        public required string ProductId { get; set; }
        public required int Quantity { get; set; }
        public bool FinalizedPurchase { get; set; } = false;
    }
}
