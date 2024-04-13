using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Controllers
{ //TODO
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
        public async Task<ActionResult> MakePurchase(PostAccuntProductModel postAccuntProductModel)
        {
            Product Product = postAccuntProductModel.Product;
            //Account Account = postAccuntProductModel.Account;
            return BadRequest();

        }
       
    }
}
