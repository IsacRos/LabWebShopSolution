using System.ComponentModel.DataAnnotations;

namespace LabWebShop.Services
{
    public interface IAccountService
    {
        Task<string> Login([Required][EmailAddress] string email, [Required] string password, string? returnUrl);
        Task Logout();
    }
}
