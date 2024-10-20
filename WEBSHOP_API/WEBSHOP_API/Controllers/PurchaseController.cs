using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEBSHOP_API.Database;
using WEBSHOP_API.DTOs;
using WEBSHOP_API.Repository.RepositoryInterface;


namespace WEBSHOP_API.Controllers
{ //TODO REWRITE this whole thing is a mess
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICartRepository _cartRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IProductRepository _productRepository;

        public PurchaseController(ILogger<PurchaseController> logger, ICartRepository cartRepository, IStockRepository stockRepository, IUserDataRepository userDataRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _cartRepository = cartRepository;
            _stockRepository = stockRepository;
            _userDataRepository = userDataRepository;
            _productRepository = productRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> StartPurchase()
        {
            try
            {
                var result = new CartValueDTO();
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                result.Value = await _cartRepository.GetCartVaule(uId);
                result.CurrencyCode = "HUF";
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
        public async Task<IActionResult> ConfirmPurchase()
        {
            try
            {
                var rnd = new Random();
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var cart = await _cartRepository.CartDataById(uId);
                var userData = await _userDataRepository.GetUserDataById(uId);

                if (cart.ProductIds.Count != 0)
                {
                   int random = rnd.Next(0, cart.ProductIds.Count);
                    for (int i = 0; i < cart.ProductIds.Count; i++)
                    {
                        await _stockRepository.UpdateStock(cart.ProductIds[i], -cart.ProductCount[i]);

                    }
                    string productCategory = await _productRepository.GetProductCategoryById(cart.ProductIds[random]);
                    userData.UserLastPurchaseCategory = productCategory;
                    await _userDataRepository.UpdateUserData(userData);
                    return NoContent();
                }
                else {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Somethig is wrong with your cart.");
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
