using LabWebShop.Client.Classes;
using LabWebShop.Models;
using LabWebShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabWebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CheckOutController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{OrderId}")]
        public async Task<ActionResult<UserCredentials>> GetCredentials(string OrderId)
        {
            var creds = await _cartService.GetCredentials(OrderId);
            return creds is not null ? Ok(creds) : BadRequest();
        }
    }
}
