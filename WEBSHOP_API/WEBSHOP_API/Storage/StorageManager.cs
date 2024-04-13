using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Storage
{
    //rewrite this, or idk now.
    public class StorageManager 
    {
        private readonly WebshopDbContext _context;
        private StorageLogger storageLog;

        public async void HideProduct(string pName) {

            var product = _context.Products.First(p => p.ProductName == pName);
            if (StockExists(product.ProductId))
            {
                var productStock = _context.Products.First(s => s.ProductId == product.ProductId);
             //   productStock.ProductStock = 0;
                await _context.SaveChangesAsync();
            }
        }
        public async void ChangeStock(string pName, int changeStock)
        {
            var product = _context.Products.First(p => p.ProductName == pName);
            if (StockExists(product.ProductId)) {
                var productStock = _context.Products.First(s => s.ProductId == product.ProductId);
              //  productStock.ProductStock += changeStock;
                await _context.SaveChangesAsync();
            }
        }
        public async void Log(Product product, Account account ,int change, string reason)
        {
            storageLog.ProductId = product.ProductId;
            storageLog.AccountId = account.AccountId;
            storageLog.StockChange = change;
            storageLog.Reason = reason;
             _context.Logs.Add(storageLog);
            await _context.SaveChangesAsync();
        }
        private bool StockExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
