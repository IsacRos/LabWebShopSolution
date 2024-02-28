using LabWebShop.Models;

namespace LabWebShop.Services
{
    public interface ICheckOutService
    {
        Task CheckOut(List<ProductDto> products);
    }
}
