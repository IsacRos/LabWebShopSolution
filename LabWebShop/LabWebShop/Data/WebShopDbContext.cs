using LabWebShop.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace LabWebShop.Data
{
    public class WebShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>();
        }
    }
}
