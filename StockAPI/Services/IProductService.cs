using StockAPI.Models;

namespace StockAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);

        //PRODUCT 
        Task<Product> UpdateStockAsync(int productId);
    }
}
