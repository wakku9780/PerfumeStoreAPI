using PerfumeStoreAPI.Models;
using System.Text.Json.Serialization;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Navigation properties
    [JsonIgnore]
    public Order Order { get; set; }
    public Product Product { get; set; }
}
