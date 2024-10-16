using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Database;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Repository
{
    public class CartRepository: ICartRepository, IDisposable
    {
        private WebshopDbContext _context;
        public CartRepository(WebshopDbContext context)
        {
            _context = context;
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

        public async Task<Cart> CartDataById(string uId)
        {
            return await _context.Carts.FindAsync(uId);
        }

        public async Task CreateCart(string uId)
        {
            Cart cart = new Cart();
            cart.CartId = uId;
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCart(Cart cart)
        {
            var existingCart = await _context.Carts.FindAsync(cart.CartId);
            _context.Entry(existingCart).CurrentValues.SetValues(cart);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCart(string uId)
        {
            Cart clearCart = new Cart();
            var existingCart = await _context.Carts.FindAsync(uId);
            clearCart.CartId = existingCart.CartId;
            clearCart.ProductsId = null;
            clearCart.ProductsCounts = null;
            _context.Entry(existingCart).CurrentValues.SetValues(clearCart);
            _context.SaveChanges();
        }

        public async Task DeleteCart(string uId)
        {
            var existingCart = await _context.Carts.FindAsync(uId);
            _context.Carts.Remove(existingCart);
            _context.SaveChanges();
        }
        public async Task<int> GetCartVaule(string uId)
        {
            int value = 0;
            var existingCart = await _context.Carts.FindAsync(uId);
            Product product = new Product();
            for (int i = 0; i < existingCart.ProductsId.Count; i++)
            {
                product = await _context.Products.FindAsync(existingCart.ProductsId[i]);
                value += product.ProductPrice * existingCart.ProductsCounts[i];

            }
            return value;
        }
    }
}
