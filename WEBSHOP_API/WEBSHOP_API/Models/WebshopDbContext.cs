using Microsoft.EntityFrameworkCore;
using System;
using WEBSHOP_API.Storage;

namespace WEBSHOP_API.Models
{
    public class WebshopDbContext :DbContext
    {
        public WebshopDbContext(DbContextOptions<WebshopDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
     
        public DbSet<StorageLogger> Logs { get; set; }
        public DbSet<Stocks> Stocks { get; set; }
    }
    

    
}
