namespace StockAPI.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public int Quantity { get; set; } // + INPUT / - OUTPUT
        public string Type { get; set; } = default!; // "IN" ou "OUT"

        public Product? Product { get; set; }
    }

}
