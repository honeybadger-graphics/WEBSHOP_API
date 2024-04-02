using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Controllers
{
    [Route("api/[controller]")]
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
            Product product = postAccuntProductModel.product;
            Account account = postAccuntProductModel.account;
            return BadRequest();

        }
       
    }
}
