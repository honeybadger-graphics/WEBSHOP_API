﻿using Microsoft.AspNetCore.Authorization;
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
        return await _context.Products.OrderBy(p=>p.ProductId).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category, int page, int numberOFProductsToDispaly)
    {
        return await _context.Products.Where(p=>p.ProductCategory == category).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsIfOnSale(int page, int numberOFProductsToDispaly)
    {
        return await _context.Products.Where(p => p.IsProductOnSale == true).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsIfPromoted(int page, int numberOFProductsToDispaly)
    {
        return await _context.Products.Where(p => p.IsProductPromoted == true).OrderBy(p => p.ProductId).Skip(numberOFProductsToDispaly * page).Take(numberOFProductsToDispaly).ToListAsync();
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
    [HttpGet]
    public async Task<ActionResult<Product>> GetProduct(Product prod)
    {
        var product = await _context.Products.FirstAsync(p => p.ProductName == prod.ProductName);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    { 
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);

    }
    [HttpPost]
    public async Task<ActionResult<Product>> UpdateProduct(Product productToUpdate)
    {
        if (productToUpdate.ProductName != null && ProductExists(productToUpdate))
        {

            if (await _context.Products.FindAsync(ProductId(productToUpdate)) is Product existingProduct )
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



    }
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

    private bool ProductExists(Product product)
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

    }
}