using LabWebShop.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LabWebShop.Models;

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
}
