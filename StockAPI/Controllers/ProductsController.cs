using Microsoft.AspNetCore.Mvc;
using StockAPI.Models;
using StockAPI.Services;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _productService.GetAllAsync());

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var created = await _productService.CreateAsync(product);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("L'ID dans l'URL ne correspond pas au corps de la requête.");

            // On récupère en NoTracking dans le service => évite le double tracking
            var exists = await _productService.GetByIdAsync(id);
            if (exists == null)
                return NotFound();

            await _productService.UpdateAsync(product);
            return NoContent();
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _productService.GetByIdAsync(id);
            if (exists == null)
                return NotFound();

            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
