using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEBSHOP_API.DTOs;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserDataRepository _userDataRepository;

        public ProductController(IProductRepository productRepository, IStockRepository stockRepository, IMapper mapper, ILogger<ProductController> logger, IUserDataRepository userDataRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
            _logger = logger;
            _userDataRepository = userDataRepository;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult> GetProducts(int page, int numberOFProductsToDispaly)
        {
            try
            {
                
                return Ok(_mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _productRepository.GetProducts(page, numberOFProductsToDispaly)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetProductsByCategory(string category, int page, int numberOFProductsToDispaly)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _productRepository.GetProductsByCategory(category, page, numberOFProductsToDispaly)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
            //return await productRepository.GetProductsByCategory(category,page,numberOFProductsToDispaly);
        }

        [HttpGet]
        public async Task<ActionResult> GetProductsIfOnSale(int page, int numberOFProductsToDispaly)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _productRepository.GetProductsIfOnSale(page, numberOFProductsToDispaly)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
            //return await productRepository.GetProductsIfOnSale(page, numberOFProductsToDispaly);
        }

        [HttpGet]
        public async Task<ActionResult> GetProductsIfPromoted(int page, int numberOFProductsToDispaly)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _productRepository.GetProductsIfPromoted(page, numberOFProductsToDispaly)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
            //return await productRepository.GetProductsIfPromoted(page,numberOFProductsToDispaly);
        }

        // GET: api/Product/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            try
            {
                //throw new Exception("exection");
                return Ok(_mapper.Map<ProductDTO>(await _productRepository.GetProductById(id)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Getting a by id product went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // GET: api/Product/name
        [HttpGet("{name}")]
        public async Task<ActionResult> GetProduct(string name)
        {
            try
            {
                return Ok(_mapper.Map<ProductDTO>(await _productRepository.GetProductByName(name)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Getting a by name product went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetReccomendedProducts()
        {
            try
            {
                var claims = HttpContext.User.Claims;
                string uId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var result = await _userDataRepository.GetUserDataById(uId);
                var products = await _productRepository.GetProductsReccomendation(result.UserLastPurchaseCategory);
                return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Getting reccomended products went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // POST: api/Product
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateProductAndStock(ProductDTO product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                await _productRepository.CreateProductAndStock(_mapper.Map<Product>(product));
                _logger.LogInformation("Created a product and stock");
                return StatusCode(StatusCodes.Status200OK, "Product added and stock created");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Creating a product went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new product record");
            }
        }
        [Authorize (Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> UpdateProduct(ProductDTO productToUpdate)
        {
            try
            {
                var existingProduct = await _productRepository.GetProductById(productToUpdate.ProductId);
                if (existingProduct == null)
                {
                    return NotFound("Product not found");
                }
                await _productRepository.UpdateProduct(_mapper.Map<Product>(productToUpdate));
                _logger.LogInformation("Updating product with id: {productId}", existingProduct.ProductId);
                return StatusCode(StatusCodes.Status200OK, "Product updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Updating a product went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/Product/5
        [Authorize (Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                _logger.LogInformation("Deleting product with id: {productId}",id);
                //add stock clear
                await _stockRepository.DeleteStock(id);
                await _productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Deleting a product went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}