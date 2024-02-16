namespace LabWebShop.Models
{
    public interface IProductService
    {
        Task Add(ProductRequest request);
        Task<List<ProductDto>> GetAll();
        Task UpdateQuantity(string id, int n);
    }
}
