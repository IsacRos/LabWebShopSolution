using LabWebShop.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;

namespace LabWebShop.Data
{
    public class WebShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> Cart { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>();
            modelBuilder.Entity<CartItem>();
            modelBuilder.Entity<Purchase>();
        }
    }
}
