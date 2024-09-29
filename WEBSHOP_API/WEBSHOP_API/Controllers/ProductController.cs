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
        //private readonly WebshopDbContext _context;

        /*public ProductController(WebshopDbContext context)
        {
            _context = context;
        }
        public ProductController()
        {
            this.productRepository = new ProductRepository();
        }*/

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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


        // GET: api/Product/name
        /*[HttpGet("{name}")]
        public async Task<ActionResult<Product>> GetProduct(string name)
        {
            var product = await _context.Products.FirstAsync(p => p.ProductName == name);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetProduct(Product prod)
        {
            var product = await _context.Products.FirstAsync(p => p.ProductName == prod.ProductName);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }*/

        // POST: api/Product
       /* [HttpPost]
        public async Task<ActionResult<string>> CreateProductAndStock(Product product)
        {
            //TODO: create a method to automaticly create a record for stocks....
            _context.Products.Add(product);
            await _context.SaveChangesAsync(); 
            var existingProduct = await _context.Products.FindAsync(ProductId(product));
            Stock stock = new Stock();
            stock.ProductId = existingProduct.ProductId;
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return Ok();

        }*/
        /*[HttpPost]
        public async Task<ActionResult<string>> UpdateProduct(Product productToUpdate)
        {
            if (productToUpdate.ProductName != null && ProductExists(productToUpdate))
            {

                if (await _context.Products.FindAsync(ProductId(productToUpdate)) is Product existingProduct)
                {
                    try
                    {
                        productToUpdate.ProductId = existingProduct.ProductId;
                        _context.Entry(existingProduct).CurrentValues.SetValues(productToUpdate);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;

                    }
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return NotFound();
            }



        }*/
        // DELETE: api/Product/5
        /*[HttpDelete("{name}")]
        public async Task<IActionResult> DeleteProduct(string name)
        {
            var product = await _context.Products.FirstAsync(p => p.ProductName == name);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

       /*private bool ProductExists(Product product)
        {
            return _context.Products.Any(e => e.ProductName == product.ProductName);
        }
        private int ProductId(Product product)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.ProductName == product.ProductName);
            if (existProduct != null)
            {
                return existProduct.ProductId;
            }
            else
            {
                return -1;
            }

        }*/
    }
}