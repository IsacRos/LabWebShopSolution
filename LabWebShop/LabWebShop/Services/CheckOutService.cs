using Blazored.LocalStorage;
using LabWebShop.Client.Classes;
using LabWebShop.Data;
using LabWebShop.Models;
using Microsoft.AspNetCore.Components.Authorization;
using MongoDB.Bson;
using System.Text;
using System.Text.Json.Nodes;

namespace LabWebShop.Services
{
    public class CheckOutService : ICheckOutService
    {
        private readonly IProductService _productService;
        private readonly WebShopDbContext _context;
        private readonly ILocalStorageService _localStorage;

        public CheckOutService(IProductService productService, WebShopDbContext context, ILocalStorageService localStorage)
        {
            _productService = productService;
            _context = context;
            _localStorage = localStorage;
        }

        public async Task CheckOut(List<ProductDto> products)
        {
            foreach (var p in products)
            {
                await _productService.UpdateQuantity(p.Id, (p.Quantity * -1));
                var cart = _context.Cart.FirstOrDefault(x => x.ProductId == p.Id && !x.FinalizedPurchase);
                if (cart is not null)
                {
                    cart.FinalizedPurchase = true;
                    _context.Cart.Update(cart);
                    await _context.SaveChangesAsync();

                }
            }
        }
    }
}
