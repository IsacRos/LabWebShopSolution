using LabWebShop.Classes;
using LabWebShop.Data;
using LabWebShop.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;


namespace LabWebShop.Services;

public class ProductService : IProductService
{
    private readonly WebShopDbContext _context;
    public ProductService(WebShopDbContext context)
    {
        _context = context;
    }

    public async Task Add(ProductRequest request)
    {
        Product product = new()
        {
            ProductId = request.ProductId,
            ProductName = request.ProductName,
            Description = request.Description,
            PriceEur = request.PriceEur,
            ImgUri = request.ImgUri
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductDto>> GetAll()
    {
        var products = await _context.Products.AsNoTracking().ToListAsync();
        var viewProducts = products.Select(x => new ProductDto
        {
            Id = x.Id.ToString(),
            ProductId = x.ProductId,
            ProductName = x.ProductName,
            Description = x.Description,
            PriceEur = x.PriceEur,
            ImgUri = x.ImgUri,
            Quantity = x.Quantity
        }).ToList();
        return viewProducts;
    }

    public async Task UpdateQuantity(string id, int n)
    {
        var product = await _context.Products.FindAsync(ObjectId.Parse(id));
        if (product != null)
        {
            product.Quantity = product.Quantity + n >= 0 ? product.Quantity + n : throw new ArgumentException("Can't be less than 0");
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ProductDto> GetByProductId(string id)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
        if (product != null)
        {
            ProductDto displayProduct = new()
            {
                Id = product.Id.ToString(),
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                PriceEur = product.PriceEur,
                ImgUri = product.ImgUri,
                Quantity = product.Quantity
            };
            return displayProduct;
        }
        throw new ArgumentException("Couldn't find product");
    }
    public async Task<ProductDto> GetById(string id)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == ObjectId.Parse(id));
        if (product != null)
        {
            ProductDto displayProduct = new()
            {
                Id = product.Id.ToString(),
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                PriceEur = product.PriceEur,
                ImgUri = product.ImgUri,
                Quantity = product.Quantity
            };
            return displayProduct;
        }
        throw new ArgumentException("Couldn't find product");
    }


    public async Task<CurrencyEx?> GetCurrency()
    {
        HttpClient http = new();
        http.DefaultRequestHeaders.Add("X-Api-Key", "p3M9VIZ8/DSrLfKmnK4Xtg==scqEVRI7a47EhyFM");
        var response = await http.GetAsync("https://api.api-ninjas.com/v1/exchangerate?pair=EUR_PLN");
        if (response is not null)
        {
            var responseBody = await response.Content.ReadFromJsonAsync<CurrencyEx>();
            return responseBody;
        }
        return null;
    }
}
