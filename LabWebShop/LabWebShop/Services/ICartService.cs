using LabWebShop.Client.Classes;
using LabWebShop.Models;

namespace LabWebShop.Services
{
    public interface ICartService
    {
        Task AddToCart(ProductDto p, int quantity = 0);
        Task<List<ProductDto>?> GetItems();
        Task<List<ProductDto>> GetFromLocalStorage();
        Task RemoveFromCart(string id, int quantity);
        Task<string> AddPurchase(UserCredentials creds, string userId);
        Task<UserCredentials?> GetCredentials(string userId);
    }
}
