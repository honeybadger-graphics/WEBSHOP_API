using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Database
{
    public class WebshopDbContext : DbContext
    {
        public WebshopDbContext(DbContextOptions<WebshopDbContext> options) : base(options) { }

        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
    
}
