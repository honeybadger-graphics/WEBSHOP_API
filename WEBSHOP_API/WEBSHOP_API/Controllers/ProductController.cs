using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IStockRepository stockRepository;
        //private readonly WebshopDbContext _context;

        /*public ProductController(WebshopDbContext context)
        {
            _context = context;
        }
        public ProductController()
        {
            this.productRepository = new ProductRepository();
        }*/

        public ProductController(IProductRepository productRepository, IStockRepository stockRepository)
        {
            this.productRepository = productRepository;
            this.stockRepository= stockRepository;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult> GetProducts(int page, int numberOFProductsToDispaly)
        {
            try
            {
                return Ok(await productRepository.GetProducts(page, numberOFProductsToDispaly));
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetProductsByCategory(string category, int page, int numberOFProductsToDispaly)
        {
            try
            {
                return Ok(await productRepository.GetProductsByCategory(category, page, numberOFProductsToDispaly));
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
                return Ok(await productRepository.GetProductsIfOnSale(page, numberOFProductsToDispaly));
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
                return Ok(await productRepository.GetProductsIfPromoted(page, numberOFProductsToDispaly));
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
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                return Ok(await productRepository.GetProductById(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // GET: api/Product/name
        [HttpGet("{name}")]
        public async Task<ActionResult<Product>> GetProduct(string name)
        {
            try
            {
                return Ok(await productRepository.GetProductByName(name));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetProduct(Product prod)
        {
            try
            {
                return Ok(await productRepository.GetProductByProduct(prod));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // POST: api/Product
       [HttpPost]
        public async Task<ActionResult> CreateProductAndStock(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                await productRepository.CreateProductAndStock(product);
                return StatusCode(StatusCodes.Status200OK, "Product added and stock created");
               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
            /*//TODO: create a method to automaticly create a record for stocks....
            _context.Products.Add(product);
            await _context.SaveChangesAsync(); 
            var existingProduct = await _context.Products.FindAsync(ProductId(product));
            Stock stock = new Stock();
            stock.ProductId = existingProduct.ProductId;
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return Ok();*/

        }
       /*[HttpPost]
        public async Task<ActionResult<Product>> UpdateProduct(Product productToUpdate)
        {
            try
            {
                if (id != )
                    return BadRequest("Employee ID mismatch");

                var employeeToUpdate = await employeeRepository.GetEmployee(id);

                if (employeeToUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");

                return await productRepository.UpdateProduct(productToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }



        }*/
        // DELETE: api/Product/5
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