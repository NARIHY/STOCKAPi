using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Models;

namespace StockAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _context.Products.Include(c => c.Category).ToListAsync();

        public async Task<Product?> GetByIdAsync(int id)
            => await _context.Products.Include(c => c.Category)
                                      .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateStockAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId)
                          ?? throw new Exception("Produit introuvable");

            var movements = await _context.StockMovements
                .Where(m => m.ProductId == productId)
                .ToListAsync();

            product.Stock = movements.Sum(m => m.Quantity);

            await _context.SaveChangesAsync();
            return product;
        }
    }
}
