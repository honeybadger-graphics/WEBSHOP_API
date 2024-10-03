using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository stockRepository;
        public StocksController(IStockRepository stockRepository)
        {
            this.stockRepository = stockRepository;
        }
        // GET: api/Stock/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Stock>> GetStockByProductId(int productId)
        {
            try
            {
                return Ok(await stockRepository.GetStockByProductId(productId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateStock(int productId, int stockChange)
        {
            try
            {
                await stockRepository.UpdateStock(productId, stockChange);
                return StatusCode(StatusCodes.Status200OK, "Product stock changed");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

    }
}
