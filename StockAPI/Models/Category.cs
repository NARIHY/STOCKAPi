using System.Text.Json.Serialization;

namespace StockAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }

}
