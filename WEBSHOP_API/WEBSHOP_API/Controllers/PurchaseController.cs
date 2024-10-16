using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEBSHOP_API.Database;
using WEBSHOP_API.Repository.RepositoryInterface;


namespace WEBSHOP_API.Controllers
{ //TODO REWRITE this whole thing is a mess
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly WebshopDbContext _context;
        private readonly ILogger _logger;
        private readonly ICartRepository _cartRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IProductRepository _productRepository;

        public PurchaseController(WebshopDbContext context, ILogger<PurchaseController> logger, ICartRepository cartRepository, IStockRepository stockRepository, IUserDataRepository userDataRepository, IProductRepository productRepository)
        {
            _context = context;
            _logger = logger;
            _cartRepository = cartRepository;
            _stockRepository = stockRepository;
            _userDataRepository = userDataRepository;
            _productRepository = productRepository;
            
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> StartPurchase()
        {
            try
            {
                var claims = HttpContext.User.Claims;
                string uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var result = await _cartRepository.GetCartVaule(uId);
                return Ok(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }

        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ConfirmPurchase()
        {
            try
            {
                Random rnd = new Random();
                var claims = HttpContext.User.Claims;
                string uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var cart = await _cartRepository.CartDataById(uId);
                var userData = await _userDataRepository.GetUserDataById(uId);

                if (cart.ProductsId != null)
                {
                   int random = rnd.Next(0, cart.ProductsId.Count);
                    for (int i = 0; i < cart.ProductsId.Count; i++)
                    {
                        await _stockRepository.UpdateStock(cart.ProductsId[i], -cart.ProductsCounts[i]);

                    }
                    string productCategory = await _productRepository.GetProductCategoryById(cart.ProductsId[random]);
                    userData.UserLastPurchaseCategory = productCategory;
                    await _userDataRepository.UpdateUserData(userData);
                    return StatusCode(StatusCodes.Status200OK, "Succesful purchase!");
                }
                else {
                    return StatusCode(StatusCodes.Status400BadRequest, "Somethig is wrong with your cart.");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Something went wrong while confirming your purchase.");
            }
        }
    }
}
