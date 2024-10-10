using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEBSHOP_API.DTOs;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IStockRepository stockRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IStockRepository stockRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.stockRepository = stockRepository;
            this.mapper = mapper;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult> GetProducts(int page, int numberOFProductsToDispaly)
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await productRepository.GetProducts(page, numberOFProductsToDispaly)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetProductsByCategory(string category, int page, int numberOFProductsToDispaly)
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await productRepository.GetProductsByCategory(category, page, numberOFProductsToDispaly)));
            }
            catch (Exception)
            {
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
                return Ok(mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await productRepository.GetProductsIfOnSale(page, numberOFProductsToDispaly)));
            }
            catch (Exception)
            {
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
                return Ok(mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await productRepository.GetProductsIfPromoted(page, numberOFProductsToDispaly)));
            }
            catch (Exception)
            {
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
                return Ok(mapper.Map<ProductDTO>(await productRepository.GetProductById(id)));
            }
            catch (Exception)
            {
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
                return Ok(mapper.Map<ProductDTO>(await productRepository.GetProductByName(name)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetProduct(ProductDTO prod)
        {
            try
            {
                return Ok(mapper.Map<ProductDTO>(await productRepository.GetProductByProduct(mapper.Map<Product>(prod)))); //TODO ADD revers mapping
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // POST: api/Product
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateProductAndStock(ProductDTO product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                await productRepository.CreateProductAndStock(mapper.Map<Product>(product));
                return StatusCode(StatusCodes.Status200OK, "Product added and stock created");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UpdateProduct(ProductDTO productToUpdate)
        {
            try
            {
                var existingProduct = await productRepository.GetProductById(productToUpdate.ProductId);
                if (existingProduct == null)
                {
                    return NotFound("Product not found");
                }
                await productRepository.UpdateProduct(mapper.Map<Product>(productToUpdate));
                return StatusCode(StatusCodes.Status200OK, "Product updated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/Product/5
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                //add stock clear
                await stockRepository.DeleteStock(id);
                await productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}