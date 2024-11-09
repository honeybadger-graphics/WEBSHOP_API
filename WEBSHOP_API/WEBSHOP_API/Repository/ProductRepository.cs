using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Database;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Repository
{
    public class ProductRepository : IProductRepository, IDisposable
    {

        private WebshopDbContext _context;
        public ProductRepository(WebshopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts(int page, int numberOFProductsToDisplay)
        {
             
            return await _context.Products.OrderBy(p => p.ProductId).Skip(numberOFProductsToDisplay * (page-1)).Take(numberOFProductsToDisplay).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByCategory(string category, int page, int numberOFProductsToDisplay)
        {
            return await _context.Products.Where(p => p.ProductCategory == category).OrderBy(p => p.ProductId).Skip(numberOFProductsToDisplay * (page-1)).Take(numberOFProductsToDisplay).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsIfOnSale(int page, int numberOFProductsToDisplay)
        {
            return await _context.Products.Where(p => p.IsProductOnSale == true).OrderBy(p => p.ProductId).Skip(numberOFProductsToDisplay * (page-1)).Take(numberOFProductsToDisplay).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsIfPromoted(int page, int numberOFProductsToDisplay)
        {
            return await _context.Products.Where(p => p.IsProductPromoted == true).OrderBy(p => p.ProductId).Skip(numberOFProductsToDisplay * (page-1)).Take(numberOFProductsToDisplay).ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product> GetProductByName(string productName)
        {
            return await _context.Products.FirstAsync(p => p.ProductName == productName); ;
        }
        public async Task<Product> AddProduct(Product product)
        {
            var result = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();   
            return result.Entity;
        }
        public async Task CreateProductAndStock(Product product)
        {
            var createdProduct = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            Stock stock = new Stock();
            stock.ProductId = createdProduct.Entity.ProductId; //wrong id for product id
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product productToUpdate)
        {
            var existingProduct = await _context.Products.FindAsync(productToUpdate.ProductId);
            _context.Entry(existingProduct).CurrentValues.SetValues(productToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            Product product = await _context.Products.FindAsync(productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Product>> GetProductsReccomendation(string category)
        {
            /*This should be something else based on many things. It just randomly skips products based on the number of products in table then takes next 5.
            This should use ML and training data to calculate which I dont have the time to fully implement for now.... maybe later?*/
            int numberOfProductToRecommend = 5;
            int count = await GetProductCount();
            int randomCeiling = 0;
            if (count > numberOfProductToRecommend)
            {
                randomCeiling = count + 1 - numberOfProductToRecommend;
            }
            else
            {
                randomCeiling = 1;
            }
            Random rnd = new Random();
            int randomSkip = rnd.Next(0, randomCeiling);
            return await _context.Products.Where(p => p.ProductCategory == category).OrderBy(p => p.ProductId).Skip(randomSkip).Take(numberOfProductToRecommend).ToListAsync();
        }

        public async Task<int> GetProductCount()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<string> GetProductCategoryById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product.ProductCategory;
            
        }
    }

}
