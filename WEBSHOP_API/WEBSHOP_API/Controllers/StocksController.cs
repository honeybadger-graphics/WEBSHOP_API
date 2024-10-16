using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WEBSHOP_API.Models;
using WEBSHOP_API.DTOs;
using WEBSHOP_API.Repository.RepositoryInterface;
using Microsoft.AspNetCore.Authorization;

namespace WEBSHOP_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize (Roles = "Admin")]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public StocksController(IStockRepository stockRepository, IMapper mapper, ILogger<StocksController> logger)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: api/Stock/id
        [HttpGet("{productId:int}")]
        public async Task<ActionResult> GetStockByProductId(int productId)
        {
            try
            {
                return Ok(_mapper.Map<StockDTO>(await _stockRepository.GetStockByProductId(productId)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetLowStocks(int stockToCompereTo)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Stock>, List<StockDTO>>(await _stockRepository.LowStockFinder(stockToCompereTo)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong: {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateStock(int productId, int stockChange)
        {
            try
            {
                await _stockRepository.UpdateStock(productId, stockChange);
                _logger.LogInformation("Updated product stock on productId {0}", productId);
                return StatusCode(StatusCodes.Status200OK, "Product stock changed");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Updating stocks went wrong on productId {0}: {error}",productId, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

    }
}
