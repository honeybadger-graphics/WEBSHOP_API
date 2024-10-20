
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEBSHOP_API.Database;
using WEBSHOP_API.DTOs;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;


namespace WEBSHOP_API.Controllers
{ //TODO: redo when account is good seriusly
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CartController(ICartRepository cartRepository, IMapper mapper, ILogger<CartController> logger)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _logger = logger;
            
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CartDTO>>GetCart()
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return Ok(_mapper.Map<CartDTO>(await _cartRepository.CartDataById(uId)));
            }
            catch (Exception e) {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateCart(CartDTO cartDTO)
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                await _cartRepository.CreateCart(_mapper.Map<Cart>(cartDTO));
                return StatusCode(StatusCodes.Status200OK, "Cart created!");
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
        public async Task<ActionResult> UpdateCart(CartDTO cartDTO)
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                cartDTO.CartId = uId;
                await _cartRepository.UpdateCart(_mapper.Map<Cart>(cartDTO));
                return StatusCode(StatusCodes.Status200OK, "Cart updated!");
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
        public async Task<ActionResult> EmtpyCart()
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                await _cartRepository.ClearCart(uId);
                return StatusCode(StatusCodes.Status200OK, "Cart cleared!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteCart()
        {
            try
            {
                var claims = HttpContext.User.Claims;
                var uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                await _cartRepository.DeleteCart(uId);
                return StatusCode(StatusCodes.Status200OK, "Cart deleted!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

    }
}
