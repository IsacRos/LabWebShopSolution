using Blazored.LocalStorage;
using LabWebShop.Client.Classes;
using LabWebShop.Data;
using LabWebShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;


namespace LabWebShop.Services
{
    public class CartService : ICartService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly WebShopDbContext _context;
        private readonly ILocalStorageService _localStorage;
        private readonly IProductService _productService;

        public CartService(SignInManager<ApplicationUser> signInManager,  WebShopDbContext context, ILocalStorageService localStorage, IProductService productService)
        {
            _signInManager = signInManager;
            _context = context;
            _localStorage = localStorage;
            _productService = productService;
        }
        public async Task AddToCart(ProductDto p, int quantity = 0)
        {
            var user = _signInManager.Context.User.Identity;
            var prod = await _productService.GetById(p.Id);
            var prodQuantity = prod?.Quantity ?? 0;
            var finalQuantity = quantity > prodQuantity ? prodQuantity : quantity;
            if (user is not null && user.IsAuthenticated)
            {
                var identity = System.Security.Claims.ClaimTypes.NameIdentifier;
                var userId = _signInManager.Context.User.FindFirst(identity)?.Value;
                var userCart = await _context.Cart.AsNoTracking().FirstOrDefaultAsync(x => 
                    !x.FinalizedPurchase && x.UserId.Equals(userId) && p.Id.Equals(x.ProductId));

                if (userCart != null)
                {
                    userCart.Quantity = finalQuantity + userCart.Quantity > prodQuantity ? prodQuantity : finalQuantity + userCart.Quantity;
                    _context.Cart.Update(userCart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    CartItem cart = new()
                    {
                        Id = ObjectId.GenerateNewId(),
                        UserId = userId ?? throw new ArgumentNullException("No user"),
                        ProductId = p.Id,
                        Quantity = finalQuantity 
                    };
                    await _context.Cart.AddAsync(cart);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                int localStorageIndex = await _localStorage.LengthAsync();
                p.Quantity = finalQuantity;
                await _localStorage.SetItemAsync(localStorageIndex.ToString(), p);
            }
            
        }

        public async Task<List<ProductDto>?> GetItems()
        {
            List<ProductDto> productList = new();
            var user = _signInManager.Context.User.Identity;
            if (user is not null && user.IsAuthenticated)
            {
                var identity = System.Security.Claims.ClaimTypes.NameIdentifier;
                var userId = _signInManager.Context.User.FindFirst(identity)?.Value;

                var userCart = await _context.Cart.Where(x => !x.FinalizedPurchase && x.UserId.Equals(userId)).ToListAsync();
                if (userCart is not null)
                {
                    productList = userCart.Select(x =>
                    {
                        var item = _productService.GetById(x.ProductId).Result;
                        return new ProductDto()
                        {
                            Id = item.Id,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Description = item.Description,
                            PriceEur = item.PriceEur,
                            ImgUri = item.ImgUri,
                            Quantity = x.Quantity
                        };
                    }).ToList();

                }
            }
            return productList;
        }
        public async Task RemoveFromCart(string id, int quantity)
        {
            var item = await _context.Cart.FirstOrDefaultAsync(x => x.ProductId == id);
            if (item is not null)
            {
                if (item.Quantity > quantity)
                {
                    item.Quantity -= quantity;
                    _context.Cart.Update(item);
                }
                else _context.Remove(item);
                await _context.SaveChangesAsync();
            }            
        }

        public async Task<List<ProductDto>> GetFromLocalStorage()
        {
            List<ProductDto> list = new();
            int localStorageIndex = await _localStorage.LengthAsync();
            for (int i = 0; i < localStorageIndex; i++)
            {
                var item = await _localStorage.GetItemAsync<ProductDto>(i.ToString());
                if (item is not null)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public async Task<string> AddPurchase(UserCredentials creds, string userId)
        {
            ObjectId id = ObjectId.GenerateNewId();
            Purchase purchase = new()
            {
                Id = id,
                UserId = userId,
                FirstName = creds.FirstName,
                LastName = creds.LastName,
                Address = creds.Address,
                PhoneNumber = creds.PhoneNumber,
                Creditcard = creds.Creditcard
            };
            await _context.Purchases.AddAsync(purchase);
            await _context.SaveChangesAsync();
            return id.ToString();
        }
        public async Task<UserCredentials?> GetCredentials(string orderId)
        {
            var purchase = await _context.Purchases.FirstOrDefaultAsync(x => x.Id == ObjectId.Parse(orderId));
            if (purchase is not null)
            {
                UserCredentials creds = new()
                {
                    FirstName = purchase.FirstName,
                    LastName = purchase.LastName,
                    Address = purchase.Address,
                    PhoneNumber = purchase.PhoneNumber,
                    Creditcard = purchase.Creditcard
                };
                return creds;
            }
            return null;
        }
    }
}
