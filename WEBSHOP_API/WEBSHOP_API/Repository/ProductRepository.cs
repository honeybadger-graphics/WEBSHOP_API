using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Repository
{
    public class ProductRepository : IProductRepository, IDisposable
    {

        private WebshopDbContext context;
        ProductRepository(WebshopDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts(int page, int numberOFProductsToDispaly)
        {
             
            return await context.Products.OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByCategory(string category, int page, int numberOFProductsToDispaly)
        {
            return await context.Products.Where(p => p.ProductCategory == category).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsIfOnSale(int page, int numberOFProductsToDispaly)
        {
            return await context.Products.Where(p => p.IsProductOnSale == true).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsIfPromoted(int page, int numberOFProductsToDispaly)
        {
            return await context.Products.Where(p => p.IsProductPromoted == true).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
        }

        public Product GetProductById(int productId)
        {
            return context.Products.Find(productId);
        }

        public Product GetProductByName(string productName)
        {
            return context.Products.First(p => p.ProductName == productName); ;
        }

        public Product GetProductByProduct(Product product)
        {
            return context.Products.First(p => p.ProductName == product.ProductName);
        }

        public void CreateProductAndStock(Product product)
        {
            return; //implement it somehow
        }

        public void UpdateProduct(Product productToUpdate)
        {
            context.Entry(productToUpdate).State = EntityState.Modified;
        }

        public void DeleteProduct(int productId)
        {
            Product product = context.Products.Find(productId);
            context.Products.Remove(product);
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
