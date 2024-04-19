using Microsoft.EntityFrameworkCore;
using System;
using WEBSHOP_API.Helpers;
using WEBSHOP_API.Storage;

namespace WEBSHOP_API.Models
{
    public class WebshopDbContext :DbContext
    {
        public WebshopDbContext(DbContextOptions<WebshopDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<StorageLogger> Logs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
    

    
}
