using LabWebShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LabWebShop.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.Route("/api/Login")]
        public async Task<ActionResult> Login([FromForm] string email,[FromForm] string password)
        {
            ApplicationUser? appUser = await _userManager.FindByEmailAsync(email);
            if (appUser is not null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName ?? "", password, false, false);

                if (result.Succeeded)
                { 
                    return LocalRedirect("/");
                }
                return BadRequest("Invalid username or Password");
            }
            return BadRequest("Couldn't find user");
        }

        [HttpPost]
        [AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.Route("/api/LogOut")]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }

    }
}
