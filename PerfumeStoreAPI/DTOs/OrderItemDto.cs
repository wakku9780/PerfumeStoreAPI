namespace PerfumeStoreAPI.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }  // Ensure OrderId is added here
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Ensure Product property is added here
        public ProductDto? Product { get; set; }
    }
}
