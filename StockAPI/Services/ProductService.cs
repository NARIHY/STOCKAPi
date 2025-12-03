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
        {
            return await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .AsNoTracking() 
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            var existing = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (existing == null) return;

            // Copie proprement les valeurs
            _context.Entry(existing).CurrentValues.SetValues(product);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _context.Products.FindAsync(id);
            if (p != null)
            {
                _context.Products.Remove(p);
                await _context.SaveChangesAsync();
            }
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

