using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Database;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Repository
{
    public class StockRepository : IStockRepository, IDisposable
    {
        private WebshopDbContext _context;
        public StockRepository(WebshopDbContext context)
        {
            _context = context;
        }
        public async Task DeleteStock(int productId)
        {
            Stock productStock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
            _context.Stocks.Remove(productStock);
            await _context.SaveChangesAsync();
        }
        public async Task<Stock> GetStockByProductId(int productId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
        }
        public async Task UpdateStock(int productId , int stockChange)
        {
            Stock productStock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
            productStock.ProductStocks += stockChange;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Stock>> LowStockFinder(int stockToCompereTo)
        {
            return await _context.Stocks.Where(s => s.ProductStocks < stockToCompereTo).ToListAsync();
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

    }
}
