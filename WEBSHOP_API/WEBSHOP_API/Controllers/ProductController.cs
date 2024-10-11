﻿using AutoMapper;
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
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductController(IProductRepository productRepository, IStockRepository stockRepository, IMapper mapper, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult> GetProducts(int page, int numberOFProductsToDispaly)
        {
            try
            {
                _logger.LogInformation("Getting Products");
                return Ok(_mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _productRepository.GetProducts(page, numberOFProductsToDispaly)));
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
                return Ok(_mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _productRepository.GetProductsByCategory(category, page, numberOFProductsToDispaly)));
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
                return Ok(_mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _productRepository.GetProductsIfOnSale(page, numberOFProductsToDispaly)));
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
                return Ok(_mapper.Map<IEnumerable<Product>, List<ProductDTO>>(await _productRepository.GetProductsIfPromoted(page, numberOFProductsToDispaly)));
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
                throw new Exception("exection");
                return Ok(_mapper.Map<ProductDTO>(await _productRepository.GetProductById(id)));
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Something went wrong: {error}", e.Message);
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // POST: api/Product
        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
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
                return StatusCode(StatusCodes.Status200OK, "Product added and stock created");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }
        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
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
                return StatusCode(StatusCodes.Status200OK, "Product updated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/Product/5
        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                //add stock clear
                await _stockRepository.DeleteStock(id);
                await _productRepository.DeleteProduct(id);
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