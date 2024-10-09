
using Microsoft.AspNetCore.Mvc;
using WEBSHOP_API.Helpers;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Controllers
{ //TODO: redo when account is good
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly WebshopDbContext _context;

        public CartController(WebshopDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<string>> UpdateCart(CartHelper helper)
        {
            var existingCart = await _context.Carts.FindAsync(AccountId(helper.creds));
            if (existingCart != null)
            {
                existingCart.ProductsId = helper.UpdateCart.ProductsId;
                existingCart.ProductsCounts = helper.UpdateCart.ProductsCounts;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else { return BadRequest(); }
        }

        [HttpPost]
        public async Task<ActionResult<string>> EmtpyCart(LoginCreds creds)
        {
            var existingCart = await _context.Carts.FindAsync(AccountId(creds));
            if (existingCart != null)
            {
                existingCart.ProductsId = null;
                existingCart.ProductsCounts = null;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else { return BadRequest(); }
        }

        private int AccountId(LoginCreds account)
        {
            var existAccount = _context.Accounts.FirstOrDefault(a => a.AccountEmail == account.Email && a.AccountPassword == account.Password);
            if (existAccount != null)
            {
                return existAccount.AccountId;
            }
            else
            {
                return -1;
            }

        }
    }
}
