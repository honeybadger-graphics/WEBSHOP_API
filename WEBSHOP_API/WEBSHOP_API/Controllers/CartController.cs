using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly WebshopDbContext _context;

        public CartController(WebshopDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<string>> UpdateCart(Cart cart)
        {
            var existingCart = await _context.Carts.FindAsync(cart.CartId);
            if (existingCart != null)
            {
                existingCart.ProductsName = cart.ProductsName;
                existingCart.ProductsCounts = cart.ProductsCounts;
                await _context.SaveChangesAsync();
                return Ok();
            }else { return BadRequest(); }
           

        }
        [HttpPost]
        public async Task<ActionResult<string>> EmtpyCart(int cartID)
        {
            var existingCart = await _context.Carts.FindAsync(cartID);
            if (existingCart != null)
            {
                existingCart.ProductsName =null;
                existingCart.ProductsCounts = null;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else { return BadRequest(); }


        }
    }
}
