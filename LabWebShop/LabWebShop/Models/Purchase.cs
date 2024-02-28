using LabWebShop.Client.Classes;
using MongoDB.Bson;

namespace LabWebShop.Models
{
    public class Purchase
    {
        public ObjectId Id { get; set; }
        public required string UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Creditcard { get; set; } = string.Empty;
    }
}
