using LabWebShop.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LabWebShop.Services
{
    public class IdentityService : IIdentityService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<List<string>> CreateUser(User user)
        {
            List<string> message = new();

            ApplicationUser appUser = new()
            {
                UserName = user.Name,
                Email = user.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
            if (result.Succeeded)
            {
                message.Add("User created successfully");
            }
            else
            {
                foreach (IdentityError e in result.Errors)
                {
                    message.Add($"{e.Code}:  {e.Description}");
                }
            }
            return message;
        }
        public async Task<List<string>> CreateRole([Required] string name)
        {
            List<string> message = new();

            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = name });
            if (result.Succeeded)
            {
                message.Add("Role created successfully");
            }
            else
            {
                foreach (IdentityError e in result.Errors)
                {
                    message.Add($"{e.Code}:  {e.Description}");
                }
            }
            return message;
        }
    }
}
