using LabWebShop.Classes;
using LabWebShop.Models;

namespace LabWebShop.Services
{
    public interface IProductService
    {
        Task Add(ProductRequest request);
        Task<List<ProductDto>> GetAll();
        Task UpdateQuantity(string id, int n);
        Task<ProductDto> GetByProductId(string id);
        Task<ProductDto> GetById(string id);
        Task<CurrencyEx?> GetCurrency();
    }
}
