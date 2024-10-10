using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WEBSHOP_API.Database;
using WEBSHOP_API.Helpers;
using WEBSHOP_API.Models;


namespace WEBSHOP_API.Controllers
{ //TODO REWRITE this whole thing is a mess
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
              
                for (int i = 0; i < cart.ProductsId.Count; i++)
                {
                    logger = new StorageLogger(AccountId(account), cart.ProductsId[i], -cart.ProductsCounts[i], "Purchase"); //WTF? Should add it but not now coz rewrites
                    var existingStock = await _context.Stocks.FirstAsync(s => s.ProductId == cart.ProductsId[i]);
                    existingStock.ProductStocks -= cart.ProductsCounts[i]; 
                    _context.SaveChanges();

                }
                cart.ProductsId = null; 
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
