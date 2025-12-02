using Microsoft.AspNetCore.Mvc;
using StockAPI.Data;
using StockAPI.Models;
using StockAPI.Services;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockMovementsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public StockMovementsController(AppDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StockMovement movement)
        {
            _context.StockMovements.Add(movement);
            await _context.SaveChangesAsync();

            
            await _productService.UpdateStockAsync(movement.ProductId);

            return Ok(movement);
        }
    }

}
