using System.Text.Json.Serialization;

namespace StockAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        // Calcul appart from movements
        public int Stock { get; set; }
    }
}
