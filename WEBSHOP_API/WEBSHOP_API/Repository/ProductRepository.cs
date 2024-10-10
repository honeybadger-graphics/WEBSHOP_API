using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WEBSHOP_API.Database;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Repository
{
    public class ProductRepository : IProductRepository, IDisposable
    {

        private WebshopDbContext context;
        public ProductRepository(WebshopDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts(int page, int numberOFProductsToDispaly)
        {
             
            return await context.Products.OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * (page-1)).Take(numberOFProductsToDispaly).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByCategory(string category, int page, int numberOFProductsToDispaly)
        {
            return await context.Products.Where(p => p.ProductCategory == category).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * (page-1)).Take(numberOFProductsToDispaly).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsIfOnSale(int page, int numberOFProductsToDispaly)
        {
            return await context.Products.Where(p => p.IsProductOnSale == true).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * (page-1)).Take(numberOFProductsToDispaly).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsIfPromoted(int page, int numberOFProductsToDispaly)
        {
            return await context.Products.Where(p => p.IsProductPromoted == true).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * (page-1)).Take(numberOFProductsToDispaly).ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await context.Products.FindAsync(productId);
        }

        public async Task<Product> GetProductByName(string productName)
        {
            return await context.Products.FirstAsync(p => p.ProductName == productName); ;
        }
        // possibly not needed
        public async Task<Product> GetProductByProduct(Product product)
        {
            return await context.Products.FirstAsync(p => p.ProductName == product.ProductName);
        }
        public async Task<Product> AddProduct(Product product)
        {
            var result = await context.Products.AddAsync(product);
            await context.SaveChangesAsync();   
            return result.Entity;
        }
        public async Task CreateProductAndStock(Product product)
        {
            var createdProduct = await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            Stock stock = new Stock();
            stock.ProductId = createdProduct.Entity.ProductId; //wrong id for product id
            await context.Stocks.AddAsync(stock);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product productToUpdate)
        {
            var existingProduct = await context.Products.FindAsync(productToUpdate.ProductId);
            context.Entry(existingProduct).CurrentValues.SetValues(productToUpdate);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            Product product = await context.Products.FindAsync(productId);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
    }

}
