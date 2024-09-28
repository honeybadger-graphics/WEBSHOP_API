using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WEBSHOP_API.Helpers;
using WEBSHOP_API.Models;


namespace WEBSHOP_API.Controllers
{ //TODO REWRITE and create special purchase
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly WebshopDbContext _context;

        public PurchaseController(WebshopDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        //rewrite this...... take logincreds not account
        public async Task<ActionResult> MakePurchase(Account account)
        {
            var existingAccount = await _context.Accounts.FindAsync(AccountId(account));
            var cart = await _context.Carts.FindAsync(existingAccount.Cart.CartId);
            StorageLogger logger; 
            if (existingAccount != null && cart != null) {
              
                for (int i = 0; i < cart.ProductsName.Count; i++)
                {
                    logger = new StorageLogger(AccountId(account), ProductId(cart.ProductsName[i]), -cart.ProductsCounts[i], "Purchase");
                    var existingStock = await _context.Stocks.FirstAsync(s => s.ProductId == ProductId(cart.ProductsName[i]));
                    existingStock.ProductStocks -= cart.ProductsCounts[i]; 
                    _context.SaveChanges();

                }
                cart.ProductsName = null; 
                cart.ProductsCounts = null;
                _context.SaveChanges();
                return Ok();

            }
            else { 
                return BadRequest(); 
            }

        }


        private int AccountId(Account account)
        {
            var existAccount = _context.Accounts.FirstOrDefault(a => a.AccountEmail == account.AccountEmail && a.AccountPassword == account.AccountPassword);
            if (existAccount != null)
            {
                return existAccount.AccountId;
            }
            else
            {
                return -1;
            }

        }
        private int ProductId(string productName)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.ProductName == productName);
            if (existProduct != null)
            {
                return existProduct.ProductId;
            }
            else
            {
                return -1;
            }

        }
    }
}
