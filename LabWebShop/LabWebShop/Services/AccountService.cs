using LabWebShop.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LabWebShop.Services
{
    public class AccountService : IAccountService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private NavigationManager _navigator;
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> Login([EmailAddress, Required] string email, [Required] string password, string? returnUrl)
        {
            ApplicationUser? appUser = await _userManager.FindByEmailAsync(email);
            if (appUser != null) 
            {
                //var pwd = await _userManager.CheckPasswordAsync(appUser, password);
                SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, password, false, false);

                if (result.Succeeded)
                {
                    return "Logged in successfully";
                }
                return "Invalid email or password";
               
            }
            return "Couldn't find user";
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
