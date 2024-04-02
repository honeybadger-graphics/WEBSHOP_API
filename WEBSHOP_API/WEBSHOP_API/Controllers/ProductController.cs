using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using WEBSHOP_API.Models;


//TODO: Move stock storage back to product => rewrite some stuff (make it less confusing to me and things)
// this will make my life easier.....
[ApiController]
[Route("api/[controller]/[Action]")]
public class ProductController : ControllerBase
{
    private readonly WebshopDbContext _context;

    public ProductController(WebshopDbContext context)
    {
        _context = context;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int page, int numberOFProductsToDispaly)
    {
        return await _context.Products.Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category, int page, int numberOFProductsToDispaly)
    {
        return await _context.Products.Where(p=>p.ProductCategory == category).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsIfOnSale(int page, int numberOFProductsToDispaly)
    {
        return await _context.Products.Where(p => p.IsProductOnSale == true).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsIfPromoted(int page, int numberOFProductsToDispaly)
    {
        return await _context.Products.Where(p => p.IsProductPromoted == true).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
    }


    // GET: api/Product/name
    [HttpGet("{name}")]
    public async Task<ActionResult<Product>> GetProduct(string name)
    {
        var product = await _context.Products.FirstAsync(p => p.ProductName == name);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(PostAccuntProductModel postAccuntProductModel)
    {
        Product product = postAccuntProductModel.product;
        Account account = postAccuntProductModel.account;
        if (account.IsAdmin) {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);

        }
        else
        {
            return NoContent();
        }


    }
    [HttpPost]
    public async Task<ActionResult<Product>> UpdateProduct(Product product)
    {
        if (product.ProductName != null && ProductExists(product.ProductName))
        {
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }
            return product;
        }
        else
        {
            return NotFound();
        }

    }
    //same problem as in account, should delete this crap
    //// PUT: api/Product/5
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutProduct(int id, Product product)
    //{
    //    if (id != product.ProductId)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(product).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!ProductExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}

    // DELETE: api/Product/5
    [HttpDelete("{name}")]
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
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }
    private bool ProductExists(string name)
    {
        return _context.Products.Any(e => e.ProductName == name);
    }
}