using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WEBSHOP_API.Helpers;
using WEBSHOP_API.Models;
using WEBSHOP_API.Storage;


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
        public async Task<ActionResult> MakePurchase(Account account)
        {
            var existingAccount = await _context.Accounts.FindAsync(AccountId(account));
            StorageLogger logger; 
            if (existingAccount != null && existingAccount.Cart != null) {
                foreach (CartHelper helper in existingAccount.Cart) {
                    var existingStocks = await _context.Stocks.FindAsync(ProductId(helper.Products));
                    logger = new StorageLogger(existingAccount.AccountId, helper.Products.ProductId, -helper.ProductsCounts, "Purchase");
                    existingStocks.ProductStocks -= helper.ProductsCounts;
                    _context.Logs.Add(logger);
                    _context.SaveChanges();

                } ;
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
        private int ProductId(Product product)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.ProductName == product.ProductName);
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
