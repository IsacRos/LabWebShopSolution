using LabWebShop.Models;
using System.ComponentModel.DataAnnotations;

namespace LabWebShop.Services
{
    public interface IIdentityService
    {
        Task<List<string>> CreateUser(User user);
        Task<List<string>> CreateRole([Required] string name);
    }
}
