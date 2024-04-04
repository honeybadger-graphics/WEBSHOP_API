using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PurchesController : ControllerBase
    {
        private readonly WebshopDbContext _context;

        public PurchesController(WebshopDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> MakePurches(PostAccuntProductModel postAccuntProductModel)
        {
            Product Product = postAccuntProductModel.Product;
            Account Account = postAccuntProductModel.Account;
            return BadRequest();

        }
       
    }
}
